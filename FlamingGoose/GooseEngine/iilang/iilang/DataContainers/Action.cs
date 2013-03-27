using System;
using System.Xml.Serialization;

namespace iilang
{

	[XmlRoot("action")]
	public class Action : DataContainer
	{
		public override string XmlTag { get { return "action"; } }
		public override string ChildXmlTag { get { return "actionParameter"; } }
		
		public Action () {
		}
		
		public Action (string name, params Parameter[] ps) 
		: base (name, ps) { }
	}

}

