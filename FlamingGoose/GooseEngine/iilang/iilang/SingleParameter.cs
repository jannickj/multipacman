using System;

namespace iilang
{
	public abstract class SingleParameter : IILangElement
	{
		public SingleParameter ()
		{
		}

		#region implemented abstract members of IILangElement

		public override void ReadXml (System.Xml.XmlReader reader)
		{
			throw new NotImplementedException ();
		}

		public override void WriteXml (System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement (XmlTag);
			writer.WriteEndElement ();
		}

		#endregion
	}
}

