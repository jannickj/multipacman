using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Collections;
using System.Linq;

namespace iilang
{
#pragma warning disable
    [XmlRoot("parameterList")]
	public class EisParameterList : EisMultiParameter
	{
		public override string XmlTag{ get { return "parameterList"; } }
//		public List<Parameter> Parameters { get; private set; }

		public EisParameterList () 
			: base() 
		{ }

		public EisParameterList (params EisParameter[] ps) 
			: base(ps) 
		{ }

		#region implemented abstract members of IILangElement

		public override void ReadXml (System.Xml.XmlReader reader)
		{
			reader.MoveToContent ();
			base.ReadXml (reader);
		}

		public override void WriteXml (System.Xml.XmlWriter writer)
		{
			base.WriteXml (writer);
		}

		#endregion

		public override bool Equals (object obj)
		{
			if (this.GetType () != obj.GetType())
				return false;

			EisParameterList pl = (EisParameterList)obj;
			return (Parameters.SequenceEqual (pl.Parameters));
		}
	}
}

