using System.Collections.Generic;
using System.Xml.Serialization;

namespace iilang.DataContainers
{
	[XmlRoot("action")]
	public class IILAction : IILDataContainer
	{
		public IILAction()
		{
		}

		public IILAction(string name, params IILParameter[] ps)
			: base(name, ps)
		{
		}

		public IILAction(string name, LinkedList<IILParameter> ps)
			: base(name, ps)
		{
		}

		public override string XmlTag
		{
			get { return "action"; }
		}

		public override string ChildXmlTag
		{
			get { return "actionParameter"; }
		}
	}
}