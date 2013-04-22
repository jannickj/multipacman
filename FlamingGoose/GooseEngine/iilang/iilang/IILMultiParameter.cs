using System.Collections.Generic;
using System.Xml;

namespace iilang
{
	public abstract class IILMultiParameter : IILParameter
	{
		private List<IILParameter> parameters;


		public IILMultiParameter()
		{
			parameters = new List<IILParameter>();
		}

		public IILMultiParameter(IILParameter[] ps)
		{
			parameters = new List<IILParameter>(ps);
		}

		public List<IILParameter> Parameters
		{
			get { return parameters; }
		}

		public void AddParameter(IILParameter p)
		{
			parameters.Add(p);
		}

		#region IXmlSerializable implementation

		public override void ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}

			reader.ReadStartElement();
			reader.MoveToContent();

			while (reader.MoveToContent() == XmlNodeType.Element)
			{
				IILParameter p = fromString(reader.LocalName);
				p.ReadXml(reader);
				parameters.Add(p);
			}
			reader.Read();
		}

		public override void WriteXml(XmlWriter writer)
		{
			foreach (IILElement p in parameters)
			{
				writer.WriteStartElement(p.XmlTag);
				p.WriteXml(writer);
				writer.WriteEndElement();
			}
		}

		#endregion
	}
}