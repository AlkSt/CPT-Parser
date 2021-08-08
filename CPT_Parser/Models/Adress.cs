using CPT_Parser.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CPT_Parser.Models
{
    [Serializable]
    public class Adress: IXmlSerializable
    {
        public Adress()
        {
            okato = "-";
            kladr = "-";
            region = new SreializableKeyValue<string, string>("-", "-");
            district = new SreializableKeyValue<string, string>("-", "-");
            locality = new SreializableKeyValue<string, string>("-", "-");
            street = new SreializableKeyValue<string, string>("-", "-");
            lavel = "-";
            other = "-";
            readableAddress = "-";
        }
        public string okato;
        public string kladr;
        public SreializableKeyValue<string, string> region;
        public SreializableKeyValue<string, string> district;
        public SreializableKeyValue<string, string> locality;
        public SreializableKeyValue<string, string> street;
        public string lavel;
        public string other;
        public string readableAddress;

        public override string ToString()
        {
            string str = "\r\nOkato : " + okato + "\r\nKladr: " + kladr;

            if (region.Item2 != "-") str += "\r\nРегион: " + region.Item1 + " "+ region.Item2;
            if (district.Item2 != "-") str += "\r\nРайон: " + district.Item1 + ". " + district.Item2;

            if (locality.Item2 != "-") str += "\r\nЛокация: " + locality.Item1 + ". " + locality.Item2;
            if (street.Item2 != "-") str += "\r\nУлица: " + street.Item1 + ". " + street.Item2;
            if (lavel != "-") str += "\r\nДом: д. " + lavel;
            if (other != "-") str += "\r\nДополнительно: " + other;
            if (readableAddress != "-") str += "\r\nПолный адрес: " + readableAddress;
            return str;
        }

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
            XmlSerializer pairSerializer = new XmlSerializer(typeof(SreializableKeyValue<string, string>));

            writer.WriteElementString("okato", okato);
            writer.WriteElementString("kladr", kladr);
            if (region.Item2 != "-"){
                writer.WriteStartElement("region");
                writer.WriteElementString("type", region.Item1);
                writer.WriteElementString("value", region.Item2);
                writer.WriteEndElement();
            }
            if (district.Item2 != "-"){
                writer.WriteStartElement("district");
                writer.WriteElementString("type", district.Item1);
                writer.WriteElementString("value", district.Item2);
                writer.WriteEndElement();
            }
            if (locality.Item2 != "-"){
                writer.WriteStartElement("locality");
                writer.WriteElementString("type", locality.Item1);
                writer.WriteElementString("value", locality.Item2);
                writer.WriteEndElement();
            }
            if (street.Item2 != "-"){
                writer.WriteStartElement("street");
                writer.WriteElementString("type", street.Item1);
                writer.WriteElementString("value", street.Item2);
                writer.WriteEndElement();
            }
            if (lavel != "-")
                writer.WriteElementString("lavel", lavel);
            if (other != "-")
                writer.WriteElementString("other", other);
            if (readableAddress != "-")
                writer.WriteElementString("readableAddress", readableAddress.ToString());
        }
    }
}
