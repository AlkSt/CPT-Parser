using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser.Models
{

    class Zone : CadastralObject
    {
        public string registrationDate;
        public string registrationNumber;
        public (string, string) typeBound;
        public (string, string) typeZone;
        public string number;
        public SpatialData spatial;

        public override string ToString()
        {
            string str = "Регистрационный номер: " + registrationNumber
                + "\r\n\rДата регистации: " + registrationDate
                + "\r\nТип границы: " + typeBound.Item1 + " " + typeBound.Item2
                + "\r\nТип Зоны: " + typeZone.Item1 + " " + typeZone.Item2
                + "\r\n\rНомер: " + number
                +"\r\n\rИоформация о территории: " + spatial;
            return str;
        }
    }
}
