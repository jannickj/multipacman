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
	public class IILParameterList : IILMultiParameter
	{
		public override string XmlTag{ get { return "parameterList"; } }
//		public List<Parameter> Parameters { get; private set; }

		public IILParameterList () 
			: base() 
		{ }

		public IILParameterList (params IILParameter[] ps) 
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

			IILParameterList pl = (IILParameterList)obj;
			return (Parameters.SequenceEqual (pl.Parameters));
		}
	}
}

