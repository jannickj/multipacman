using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using iilang.Exceptions;

namespace iilang
{
#pragma warning disable
	public abstract class IILDataContainer : IILElement
	{
		public IILDataContainer()
		{
			Parameters = new List<IILParameter>();
		}

		public IILDataContainer(String name, IILParameter[] ps)
		{
			Name = name;
			Parameters = new List<IILParameter>(ps);
		}

		public IILDataContainer(string name, LinkedList<IILParameter> ps)
		{
			Name = name;
			Parameters = ps.ToList();
		}

		public abstract string ChildXmlTag { get; }
		public string Name { get; private set; }
		public List<IILParameter> Parameters { get; private set; }


		public virtual void TransferFrom(IILDataContainer con)
		{
			Parameters = con.Parameters;
			Name = con.Name;
		}

		public void addParameter(IILParameter par)
		{
			Parameters.Add(par);
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
				return false;

			IILDataContainer dc = (IILDataContainer) obj;
			return (Parameters.SequenceEqual(dc.Parameters) && Name.Equals(dc.Name));
		}

		#region implemented abstract members of IILangElement

		public override void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			if (reader.AttributeCount == 0)
				throw new MissingXmlAttributeException(@"Missing XML attribute ""value"".");
			Name = reader["name"];

			if (reader.IsEmptyElement)
			{
				reader.Read();
			}

			if (reader.ReadToDescendant(ChildXmlTag))
			{
				while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == ChildXmlTag)
				{
					reader.ReadStartElement();
					reader.MoveToContent();

					IILParameter p = IILParameter.fromString(reader.LocalName);
					p.ReadXml(reader);
					Parameters.Add(p);
					reader.Read();
				}
			}
			//reader.Read();
		}

		public override void WriteXml(XmlWriter writer)
		{
			if (String.IsNullOrEmpty(Name))
				throw new MissingXmlAttributeException(@"String ""Name"" must not be empty");

			writer.WriteAttributeString("name", Name);
			foreach (IILParameter p in Parameters)
			{
				writer.WriteStartElement(ChildXmlTag);
				writer.WriteStartElement(p.XmlTag);
				p.WriteXml(writer);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		#endregion
	}
}