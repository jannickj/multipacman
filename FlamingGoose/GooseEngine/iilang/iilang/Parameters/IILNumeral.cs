using System;
using System.Xml.Serialization;
using System.Xml;
using System.Globalization;

namespace iilang
{
#pragma warning disable
    [XmlRoot("number")]
	public class IILNumeral : IILParameter
	{
		public override string XmlTag { get {return "number";} }
		public double Value { get; private set; }
		
		public IILNumeral (Double value)
		{
			Value = value;
		}
		
		public IILNumeral()
		{
			Value = Double.NaN;
		}
		
		public override void WriteXml(XmlWriter writer)
		{
			if (Double.IsNaN (Value))
				throw new MissingXmlAttributeException ("Error: Value not set.");

			writer.WriteAttributeString ("value", Value.ToString());
		}
		
		public override void ReadXml (XmlReader reader)
		{
			reader.MoveToContent ();
			if (reader.AttributeCount == 0)
				throw new MissingXmlAttributeException (@"Missing XML attribute ""value"".");
            Value = Convert.ToDouble(reader["value"], CultureInfo.InvariantCulture);
			reader.Read ();
		}

		public override bool Equals (object obj)
		{
			if (this.GetType () != obj.GetType())
				return false;
			
			IILNumeral num = (IILNumeral)obj;
			return (Value == num.Value);
		}
	}
}

