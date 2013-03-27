using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections;
using System.Xml;

namespace iilang
{
	[XmlRoot("function")]
	public class Function : MultiParameter
	{
		public override string XmlTag{ get { return "function"; } }
		public string Name { get; protected set; }
//		public List<Parameter> Parameters { get; private set; }

		public Function () 
		{ 
//			Parameters = new List<Parameter> ();
		}

		public Function(String name, params Parameter[] ps)
		{
			Name = name;
//			Parameters = new List<Parameter> (ps);
		}

		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString ("name", Name);

			foreach (IILangElement p in Parameters) {
				writer.WriteStartElement(p.XmlTag);
				p.WriteXml(writer);
				writer.WriteEndElement();
			}
		}

		public override void ReadXml (System.Xml.XmlReader reader)
		{
			reader.MoveToContent ();
			Name = reader ["name"];
			base.ReadXml (reader);
		}
	}
}

