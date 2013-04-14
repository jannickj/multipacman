using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace iilang
{

	[XmlRoot("action")]
	public class IILAction : IILDataContainer
	{
		public override string XmlTag { get { return "action"; } }
		public override string ChildXmlTag { get { return "actionParameter"; } }
		
		public IILAction () {
		}
		
		public IILAction (string name, params IILParameter[] ps) 
			: base (name, ps) 
		{ 
		}

		public IILAction (string name, LinkedList<IILParameter> ps)
			: base (name, ps)
		{
		}

	}

}

