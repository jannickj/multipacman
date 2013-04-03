using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
	public abstract class EisMultiParameter : EisParameter
	{
		public List<EisParameter> Parameters { get; private set; }

		public EisMultiParameter ()
		{
			Parameters = new List<EisParameter> ();
		}

		public EisMultiParameter(EisParameter[] ps)
		{
			Parameters = new List<EisParameter> (ps);
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
				
				EisParameter p = EisParameter.fromString(reader.LocalName);
				p.ReadXml(reader);
				Parameters.Add(p);
			}
			reader.Read();
		}

		public override void WriteXml (System.Xml.XmlWriter writer)
		{
			foreach (EisIILangElement p in Parameters) {
				writer.WriteStartElement (p.XmlTag);
				p.WriteXml (writer);
				writer.WriteEndElement ();
			}
		}
		#endregion
	}
}

