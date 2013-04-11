using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace iilang
{
	[XmlRoot("percept")]
	public class EisPercept : EisDataContainer
	{
		public override string XmlTag { get { return "percept"; } }
		public override string ChildXmlTag { get { return "perceptParameter"; } }

		public EisPercept () {
		}

		public EisPercept (string name, params EisParameter[] ps) 
			: base (name, ps) 
		{ 
		}

		public EisPercept (string name, LinkedList<EisParameter> ps)
		:base (name, ps) 
		{ 
		}

	}
}

