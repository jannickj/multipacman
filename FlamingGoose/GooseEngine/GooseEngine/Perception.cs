using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GooseEngine
{
    public abstract class Percept : IXmlSerializable
    {
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            
        }

        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            
        }
    }
}
