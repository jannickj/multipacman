using System.Collections.Generic;
using System.Xml.Serialization;

namespace JSLibrary.IiLang.DataContainers
{
	[XmlRoot("percept")]
	public class IILPercept : IILDataContainer
	{
		public IILPercept()
		{
		}

		public IILPercept(string name, params IILParameter[] ps)
			: base(name, ps)
		{
		}

		public IILPercept(string name, LinkedList<IILParameter> ps)
			: base(name, ps)
		{
		}

		public override string XmlTag
		{
			get { return "percept"; }
		}

		public override string ChildXmlTag
		{
			get { return "perceptParameter"; }
		}
	}
}