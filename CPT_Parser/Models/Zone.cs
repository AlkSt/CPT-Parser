using CPT_Parser.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser.Models
{
    [Serializable]
    public class Zone : CadastralObject
    {
        public Zone() {
            typeBound = new SreializableKeyValue<string, string>();
            typeZone = new SreializableKeyValue<string, string>();
        
        }
        public string registrationDate;
        public string registrationNumber;
        public SreializableKeyValue<string,string> typeBound;
        public SreializableKeyValue<string, string> typeZone;
        public string number;
        public SpatialData spatial;

        public override string ToString()
        {
            string str = "Зона/территория"
                + "\r\n\rРегистрационный номер: " + registrationNumber
                + "\r\nДата регистации: " + registrationDate
                + "\r\nТип границы: " + typeBound.Item1 + " " + typeBound.Item2
                + "\r\nТип Зоны: " + typeZone.Item1 + " " + typeZone.Item2;
            if (number != "-") str += "\r\nНомер: " + number;
                str += "\r\n\r"+ spatial;
            return str;
        }
    }
}
