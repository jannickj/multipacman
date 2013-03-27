using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
	[XmlRoot("identifier")]
	public class Identifier : Parameter
	{
		public override string XmlTag { get {return "identifier";} }
		public string Value { get; private set; }

		public Identifier (string value)
		{
			Value = value;
		}

		public Identifier()
		{
			Value = "null";
		}

		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString ("value", Value);
		}

		public override void ReadXml (XmlReader reader)
		{
			reader.MoveToContent ();
			Value = reader ["value"];
			reader.Read ();
		}
	}
}

