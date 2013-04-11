using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace iilang
{

	[XmlRoot("action")]
	public class EisAction : EisDataContainer
	{
		public override string XmlTag { get { return "action"; } }
		public override string ChildXmlTag { get { return "actionParameter"; } }
		
		public EisAction () {
		}
		
		public EisAction (string name, params EisParameter[] ps) 
			: base (name, ps) 
		{ 
		}

		public EisAction (string name, LinkedList<EisParameter> ps)
			: base (name, ps)
		{
		}
	}

}

