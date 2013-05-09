using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using JSLibrary.IiLang.Exceptions;

namespace JSLibrary.IiLang.Parameters
{
#pragma warning disable 
	[XmlRoot("function")]
	public class IILFunction : IILMultiParameter
	{
//		public List<Parameter> Parameters { get; private set; }

		public IILFunction()
		{
//			Parameters = new List<Parameter> ();
		}

		public IILFunction(String name, params IILParameter[] ps) : base(ps)
		{
			Name = name;
//			Parameters = new List<Parameter> (ps);
		}

		public override string XmlTag
		{
			get { return "function"; }
		}

		public string Name { get; protected set; }

		public override void WriteXml(XmlWriter writer)
		{
			if (String.IsNullOrEmpty(Name))
				throw new MissingXmlAttributeException(@"String ""Name"" must not be empty");
			writer.WriteAttributeString("name", Name);

			base.WriteXml(writer);
		}

		public override void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			if (reader.AttributeCount == 0)
				throw new MissingXmlAttributeException(@"Missing XML attribute ""name"".");
			Name = reader["name"];
			base.ReadXml(reader);
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
				return false;

			IILFunction fun = (IILFunction) obj;
			return (Parameters.SequenceEqual(fun.Parameters) && Name.Equals(fun.Name));
		}
	}
}