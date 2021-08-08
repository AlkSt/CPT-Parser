using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CPT_Parser
{
    class Serializer
    {
        DataCadastralSet elementsDataSet;

        public Serializer(DataCadastralSet elementsDataSet)
        {
            this.elementsDataSet = elementsDataSet;
        }
        public string SerializeElemtnts(List<List<string>> selectedNodes)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Auto;
            settings.Indent = true;
            var serializeDoc = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(serializeDoc, settings))
            {
                writer.WriteStartElement("CadastralObjects");
                foreach (var cadastralElements in selectedNodes)
                {
                    Type elementsType = elementsDataSet.GetObjectById(cadastralElements.First()).GetType();
                    writer.WriteStartElement(elementsType.Name + "Objects");
                    foreach (var elementId in cadastralElements)
                    {
                        var element = elementsDataSet.GetObjectById(elementId);
                        XmlSerializer formatter = new XmlSerializer(element.GetType());
                        formatter.Serialize(writer, element);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return serializeDoc.ToString();
        }
    }
}
