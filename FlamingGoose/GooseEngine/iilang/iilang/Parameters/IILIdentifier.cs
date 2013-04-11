using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
#pragma warning disable
    [XmlRoot("identifier")]
	public class IILIdentifier : IILParameter
	{
		public override string XmlTag { get {return "identifier";} }
		public string Value { get; private set; }

		public IILIdentifier (string value)
		{
			Value = value;
		}

		public IILIdentifier()
		{ }

		public override void WriteXml(XmlWriter writer)
		{
			if (String.IsNullOrEmpty (Value))
				throw new MissingXmlAttributeException ("Error: Value not set.");
            
			writer.WriteAttributeString ("value", Value);
		}

		public override void ReadXml (XmlReader reader)
		{
			reader.MoveToContent ();
			Value = reader ["value"];
			reader.Read ();
		}

		public override bool Equals (object obj)
		{
			if (this.GetType () != obj.GetType())
				return false;

			IILIdentifier id = (IILIdentifier)obj;
			return (Value == id.Value);
		}
	}
}

