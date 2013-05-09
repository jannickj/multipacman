using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace JSLibrary.IiLang.Parameters
{
#pragma warning disable
	[XmlRoot("parameterList")]
	public class IILParameterList : IILMultiParameter
	{
//		public List<Parameter> Parameters { get; private set; }

		public IILParameterList()
		{
		}

		public IILParameterList(params IILParameter[] ps)
			: base(ps)
		{
		}

		#region implemented abstract members of IILangElement

		public override void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			base.ReadXml(reader);
		}

		public override void WriteXml(XmlWriter writer)
		{
			base.WriteXml(writer);
		}

		#endregion

		public override string XmlTag
		{
			get { return "parameterList"; }
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
				return false;

			IILParameterList pl = (IILParameterList) obj;
			return (Parameters.SequenceEqual(pl.Parameters));
		}
	}
}