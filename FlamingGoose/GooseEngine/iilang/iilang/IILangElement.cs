using System;
using System.Xml.Serialization;

namespace iilang
{
	public abstract class IILangElement : IXmlSerializable
	{
		public abstract string XmlTag{ get; }

		public IILangElement () {
		
		}

		#region IXmlSerializable implementation

		public System.Xml.Schema.XmlSchema GetSchema () {
			return null;
		}

		public abstract void ReadXml (System.Xml.XmlReader reader);

		public abstract void WriteXml (System.Xml.XmlWriter writer);

		#endregion
	}
}

