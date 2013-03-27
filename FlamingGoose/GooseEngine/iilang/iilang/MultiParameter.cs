using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
	public abstract class MultiParameter : Parameter
	{
		public List<Parameter> Parameters { get; private set; }

		public MultiParameter ()
		{
			Parameters = new List<Parameter> ();
		}

		public MultiParameter(Parameter[] ps)
		{
			Parameters = new List<Parameter> (ps);
		}


		#region IXmlSerializable implementation
		
		public override void ReadXml (System.Xml.XmlReader reader)
		{	
			if (reader.IsEmptyElement) {
				reader.Read ();
			}
			
			while (reader.MoveToContent() == XmlNodeType.Element) {
				
				reader.ReadStartElement();
				reader.MoveToContent();
				
				Parameter p = Parameter.fromString(reader.LocalName);
				p.ReadXml(reader);
				Parameters.Add(p);
			}
			reader.Read();
		}
		#endregion
	}
}

