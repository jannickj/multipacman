using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
	public abstract class IILMultiParameter : IILParameter
	{
		public List<IILParameter> Parameters { get; private set; }

		public IILMultiParameter ()
		{
			Parameters = new List<IILParameter> ();
		}

		public IILMultiParameter(IILParameter[] ps)
		{
			Parameters = new List<IILParameter> (ps);
		}

		public void AddParameter (IILParameter p)
		{
			Parameters.Add (p);
		}

		#region IXmlSerializable implementation
		
		public override void ReadXml (System.Xml.XmlReader reader)
		{	
			if (reader.IsEmptyElement) {
				reader.Read ();
			}

			reader.ReadStartElement();
			reader.MoveToContent();

			while (reader.MoveToContent() == XmlNodeType.Element) {
				
				IILParameter p = IILParameter.fromString(reader.LocalName);
				p.ReadXml(reader);
				Parameters.Add(p);
			}
			reader.Read();
		}

		public override void WriteXml (System.Xml.XmlWriter writer)
		{
			foreach (IILElement p in Parameters) {
				writer.WriteStartElement (p.XmlTag);
				p.WriteXml (writer);
				writer.WriteEndElement ();
			}
		}
		#endregion
	}
}

