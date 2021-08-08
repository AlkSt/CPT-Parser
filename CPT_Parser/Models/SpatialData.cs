using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CPT_Parser.Models
{
    [Serializable]
    public class SpatialData : CadastralObject
    {
        public struct Geopoint : IXmlSerializable
        {
            public Geopoint(double x, double y, object[] param)
            {
                this.x = x;
                this.y = y;

                geoNum = param[0].ToString();
                geoZcrep = param[1].ToString();
                opredCode = param[2].ToString();
                opredVal = param[3].ToString();
                delta = double.Parse(param[4].ToString());
            }
            public double x;
            public double y;

            public string geoNum;
            public string geoZcrep;
            public string opredCode;
            public string opredVal;
            public double delta;

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
                writer.WriteElementString("x", x.ToString());
                writer.WriteElementString("y", y.ToString());
                if (geoNum != "-")
                    writer.WriteElementString("geo_num", geoNum);
                if (geoZcrep != "-")
                    writer.WriteElementString("geo_zcrep", geoZcrep);
                if (opredCode != "-")
                    writer.WriteElementString("opred_code", opredCode);
                if (opredVal != "-")
                    writer.WriteElementString("opred_val", opredVal);
                if (delta != 0)
                    writer.WriteElementString("delta", delta.ToString());

            }
        }

        public string skId;
        public SerializableDictionary<int, Geopoint> Ordinates;

        public SpatialData()
        {
            Ordinates = new SerializableDictionary<int, Geopoint>();
        }

        public void AddOrdinate(double x, double y, int num = -1, params object[] param)
        {
            if (num == -1)
                num = Ordinates.Count;

            Ordinates.Add(num, new Geopoint(x, y, param));
        }

        public override string ToString()
        {
            string str = "Пространственные данные: "
                + "\r\nИдентификатор skId: " + skId;
            foreach (var ordinate in Ordinates.Keys)
            {
                str += "\r\nТочка: " + ordinate;
                str += "\r\nКоординаты: (" + Ordinates[ordinate].x + " ; " + Ordinates[ordinate].y + ")";
                if (Ordinates[ordinate].geoNum != "-") str += "\r\nНомер: " + Ordinates[ordinate].geoNum;
                if (Ordinates[ordinate].geoZcrep != "-") str += "\r\nЗакрепление: " + Ordinates[ordinate].geoZcrep;
                if (Ordinates[ordinate].opredCode != "-") str += "\r\nКод opred: " + Ordinates[ordinate].opredCode;
                if (Ordinates[ordinate].opredVal != "-") str += "\r\nЗначение opred: " + Ordinates[ordinate].opredVal;
                if (Ordinates[ordinate].delta != 0) str += "\r\nДельта: " + Ordinates[ordinate].delta;
            }

            return str;
        }

    }
}
