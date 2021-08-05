using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser.Models
{
    class Bound
    {
        public Bound()
        {
           
        }
        public string registrationDate;
        public string registrationNumber;
        public (string, string) type;
        public SpatialData spatial;
        public override string ToString()
        {
            string str = "Регистрационный номер: " + registrationNumber
                + "\r\n\rДата регистации: " + registrationDate
                + "\r\nТип границы: " + type.Item1 + " " + type.Item2
                + "\r\n\rИоформация о территории: " + spatial;
            return str;
        }
    }
}
