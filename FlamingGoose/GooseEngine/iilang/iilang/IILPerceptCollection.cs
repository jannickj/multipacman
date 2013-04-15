using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Xml;

namespace iilang
{
	[XmlRoot("perceptCollection")]
	public class IILPerceptCollection : IILElement, IXmlSerializable
	{
		private List<IILPercept> percepts = new List<IILPercept> ();
		public List<IILPercept> Percepts
		{ 
			get { return percepts; } 
		}

		public IILPerceptCollection ()
		{

		}

		public IILPerceptCollection (params IILPercept[] ps)
		{
			foreach (IILPercept p in ps)
				percepts.Add (p);
		}

//        #region IXmlSerializable implementation
//        public System.Xml.Schema.XmlSchema GetSchema ()
//        {
//            return null;
//        }

//        public void ReadXml (System.Xml.XmlReader reader)
//        {
//            // No unit tests, we are only interested in writing perceptCollections
//            throw new NotImplementedException ();
////			reader.MoveToContent ();
////			
////			if (reader.IsEmptyElement) {
////				reader.Read ();
////			}
////			
////			if (reader.ReadToDescendant ("percept")) {
////				while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "percept") {
////					
////					reader.ReadStartElement();
////					reader.MoveToContent();
////					
////					IILPercept p = new IILPercept();
////					p.ReadXml(reader);
////					Percepts.Add(p);
////					reader.Read();
////				}
////			}
////			reader.Read();		
//        }

//        public void WriteXml (System.Xml.XmlWriter writer)
//        {
//            foreach (IILPercept p in percepts) {
//                writer.WriteStartElement("percept");
//                p.WriteXml(writer);
//                writer.WriteEndElement();
//            }
//        }
//        #endregion

        public override string XmlTag
        {
            get { return "perceptCollection"; }
        }

        public override void ReadXml(XmlReader reader)
        {
            // No unit tests, we are only interested in writing perceptCollections
            throw new NotImplementedException();
            //			reader.MoveToContent ();
            //			
            //			if (reader.IsEmptyElement) {
            //				reader.Read ();
            //			}
            //			
            //			if (reader.ReadToDescendant ("percept")) {
            //				while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "percept") {
            //					
            //					reader.ReadStartElement();
            //					reader.MoveToContent();
            //					
            //					IILPercept p = new IILPercept();
            //					p.ReadXml(reader);
            //					Percepts.Add(p);
            //					reader.Read();
            //				}
            //			}
            //			reader.Read();	
        }

        public override void WriteXml(XmlWriter writer)
        {
            foreach (IILPercept p in percepts)
            {
                writer.WriteStartElement("percept");
                p.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
    }
}

