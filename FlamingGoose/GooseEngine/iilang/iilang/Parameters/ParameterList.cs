using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace iilang
{
	[XmlRoot("parameterList")]
	public class ParameterList : MultiParameter
	{
		public override string XmlTag{ get { return "parameterList"; } }
//		public List<Parameter> Parameters { get; private set; }

		public ParameterList () {
//			Parameters = new List<Parameter> ();
		}

		public ParameterList (params Parameter[] ps)
		{
//			Parameters = new List<Parameter> (ps);
		}

		#region implemented abstract members of IILangElement

		public override void ReadXml (System.Xml.XmlReader reader)
		{
			reader.MoveToContent ();
			base.ReadXml (reader);
		}

		public override void WriteXml (System.Xml.XmlWriter writer)
		{
			foreach (IILangElement p in Parameters) {
				writer.WriteStartElement(p.XmlTag);
				p.WriteXml(writer);
				writer.WriteEndElement();
			}
		}

		#endregion
	}
}

