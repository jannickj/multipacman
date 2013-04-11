using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections;
using System.Xml;
using System.Linq;

namespace iilang
{
#pragma warning disable 
    [XmlRoot("function")]
	public class IILFunction : IILMultiParameter
	{
		public override string XmlTag{ get { return "function"; } }
		public string Name { get; protected set; }
//		public List<Parameter> Parameters { get; private set; }

		public IILFunction () : base()
		{ 
//			Parameters = new List<Parameter> ();
		}

		public IILFunction(String name, params IILParameter[] ps) : base(ps)
		{
			Name = name;
//			Parameters = new List<Parameter> (ps);
		}

		public override void WriteXml(XmlWriter writer)
		{
			if (String.IsNullOrEmpty (Name)) 
				throw new MissingXmlAttributeException (@"String ""Name"" must not be empty");
			writer.WriteAttributeString ("name", Name);

			base.WriteXml (writer);
		}

		public override void ReadXml (System.Xml.XmlReader reader)
		{
			reader.MoveToContent ();
			if (reader.AttributeCount == 0)
				throw new MissingXmlAttributeException (@"Missing XML attribute ""name"".");
			Name = reader ["name"];
			base.ReadXml (reader);
		}

		public override bool Equals (object obj)
		{
			if (this.GetType () != obj.GetType())
				return false;
			
			IILFunction fun = (IILFunction)obj;
			return (Parameters.SequenceEqual (fun.Parameters) && Name.Equals(fun.Name));
		}
	}
}

