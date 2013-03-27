using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace iilang
{
	[XmlRoot("percept")]
	public class Percept : DataContainer
	{
		public override string XmlTag { get { return "percept"; } }
		public override string ChildXmlTag { get { return "perceptParameter"; } }

		public Percept () {
		}

		public Percept (string name, params Parameter[] ps) 
		: base (name, ps) { }

//		public XElement toXml ()
//		{
//			XElement xml;
//			xml = new XElement("percept", new XAttribute("name", name));
//
//			foreach (IParameter p in parameters)
//			{
//				XElement pxml;
//				pxml = new XElement("perceptParameter", p.toXml());
//				xml.Add(pxml);
//			}
//
//			return xml;
//		}
	}
}

