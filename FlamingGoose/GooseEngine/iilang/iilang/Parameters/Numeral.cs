using System;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
	[XmlRoot("number")]
	public class Numeral : Parameter
	{
		public override string XmlTag { get {return "number";} }
		public double Value { get; private set; }
		
		public Numeral (double value)
		{
			Value = value;
		}
		
		public Numeral()
		{
			Value = 0;
		}
		
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString ("value", Value.ToString());
		}
		
		public override void ReadXml (XmlReader reader)
		{
			reader.MoveToContent ();
			Value = Convert.ToDouble (reader ["value"]);
			reader.Read ();
		}
	}
}

