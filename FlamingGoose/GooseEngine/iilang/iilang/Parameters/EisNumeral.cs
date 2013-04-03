using System;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
	[XmlRoot("number")]
	public class EisNumeral : EisParameter
	{
		public override string XmlTag { get {return "number";} }
		public double Value { get; private set; }
		
		public EisNumeral (double value)
		{
			Value = value;
		}
		
		public EisNumeral()
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

		public override bool Equals (object obj)
		{
			if (this.GetType () != obj.GetType())
				return false;
			
			EisNumeral num = (EisNumeral)obj;
			return (Value == num.Value);
		}
	}
}

