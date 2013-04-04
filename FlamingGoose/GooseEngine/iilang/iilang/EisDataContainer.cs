using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Linq;

namespace iilang
{
#pragma warning disable
    public abstract class EisDataContainer : EisIILangElement
	{
		public abstract string ChildXmlTag{ get; }
		public string Name { get; private set; }
		public List<EisParameter> Parameters { get; private set; }

		public EisDataContainer () {
			Parameters = new List<EisParameter> ();
		}

		public EisDataContainer (String name, EisParameter[] ps)
		{
			Name = name;
			Parameters = new List<EisParameter> (ps);
		}

		#region implemented abstract members of IILangElement

		public override void ReadXml (System.Xml.XmlReader reader)
		{
			reader.MoveToContent ();
			if (reader.AttributeCount == 0)
				throw new MissingXmlAttributeException (@"Missing XML attribute ""value"".");
			Name = reader ["name"];

			if (reader.IsEmptyElement) {
				reader.Read ();
			}

			if (reader.ReadToDescendant (ChildXmlTag)) {
				while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == ChildXmlTag) {

					reader.ReadStartElement();
					reader.MoveToContent();

					EisParameter p = EisParameter.fromString(reader.LocalName);
					p.ReadXml(reader);
					Parameters.Add(p);
					reader.Read();
				}
			}
			reader.Read();

		}

		public override void WriteXml (System.Xml.XmlWriter writer)
		{
			if (String.IsNullOrEmpty (Name))
				throw new MissingXmlAttributeException (@"String ""Name"" must not be empty");

			writer.WriteAttributeString ("name", Name);
			foreach (EisParameter p in Parameters) {
				writer.WriteStartElement(ChildXmlTag);
				writer.WriteStartElement(p.XmlTag);
				p.WriteXml(writer);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		#endregion

		public override bool Equals (object obj)
		{
			if (this.GetType () != obj.GetType())
				return false;
			
			EisDataContainer dc = (EisDataContainer)obj;
			return (Parameters.SequenceEqual (dc.Parameters) && Name.Equals(dc.Name));
		}
	}

}

