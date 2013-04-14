using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
	public abstract class IILMultiParameter : IILParameter
	{
		private List<IILParameter> parameters;

		public List<IILParameter> Parameters { 
			get
			{
				return parameters;
			}
		}


		public IILMultiParameter ()
		{
			parameters = new List<IILParameter> ();
		}

		public IILMultiParameter(IILParameter[] ps)
		{
			parameters = new List<IILParameter> (ps);
		}

		public void AddParameter (IILParameter p)
		{
			parameters.Add (p);
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
				parameters.Add(p);
			}
			reader.Read();
		}

		public override void WriteXml (System.Xml.XmlWriter writer)
		{
			foreach (IILElement p in parameters) {
				writer.WriteStartElement (p.XmlTag);
				p.WriteXml (writer);
				writer.WriteEndElement ();
			}
		}
		#endregion
	}
}

