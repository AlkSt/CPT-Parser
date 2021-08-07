using CPT_Parser.Serialize;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser.Models
{
    [Serializable]
    public class Bound:CadastralObject
    {
        public Bound()
        {
            type = new SreializableKeyValue<string, string>();
        }
        public string registrationDate;
        public string registrationNumber;
        public SreializableKeyValue<string, string> type;
        public SpatialData spatial;
        public override string ToString()
        {
            string str = "Муниципальные границы"
                +"\r\n\rРегистрационный номер: " + registrationNumber
                + "\r\nДата регистации: " + registrationDate
                + "\r\nТип границы: " + type.Item1 + " " + type.Item2
                + "\r\n\n" + spatial;
            return str;
        }
    }
}
