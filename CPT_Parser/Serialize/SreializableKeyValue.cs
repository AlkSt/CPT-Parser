using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CPT_Parser.Serialize
{
    public class SreializableKeyValue<T1, T2> : IXmlSerializable
    {
        public SreializableKeyValue() { }
        public SreializableKeyValue(T1 item1, T2 item2) { Item1 = item1; Item2 = item2; }
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            
            if ( Item1!= null)
                writer.WriteElementString("code", Item1.ToString());
            if (Item2 != null)
                writer.WriteElementString("value", Item2.ToString());


        }
    }
}
