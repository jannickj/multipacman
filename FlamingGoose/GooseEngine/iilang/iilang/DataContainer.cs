using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;

namespace iilang
{
	public abstract class DataContainer : IILangElement
	{
		public abstract string ChildXmlTag{ get; }
		public string Name { get; private set; }
		public List<Parameter> Parameters { get; private set; }

		public DataContainer () {
			Parameters = new List<Parameter> ();
		}

		public DataContainer (String name, Parameter[] ps)
		{
			Name = name;
			Parameters = new List<Parameter> (ps);
		}

		#region implemented abstract members of IILangElement

		public override void ReadXml (System.Xml.XmlReader reader)
		{
			reader.MoveToContent ();
			Name = reader ["name"];

			if (reader.IsEmptyElement) {
				reader.Read ();
			}

			if (reader.ReadToDescendant (ChildXmlTag)) {
				while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == ChildXmlTag) {

					reader.ReadStartElement();
					reader.MoveToContent();

					Parameter p = Parameter.fromString(reader.LocalName);
					p.ReadXml(reader);
					Parameters.Add(p);
					reader.Read();
				}
			}
			reader.Read();

		}

		public override void WriteXml (System.Xml.XmlWriter writer)
		{
			writer.WriteAttributeString ("name", Name);
			foreach (Parameter p in Parameters) {
				writer.WriteStartElement(ChildXmlTag);
				writer.WriteStartElement(p.XmlTag);
				p.WriteXml(writer);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		#endregion
	}

}

