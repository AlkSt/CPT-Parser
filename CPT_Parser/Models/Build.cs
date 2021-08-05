using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser.Models
{
    class Build: Construction
    {
        public double area;
        public (string, string) adressType;
        public string okatoLoc;
        public (string, string) region;
        public string position;
        public double cost;
        public override string ToString()
        {
            string str = "Кадастровый номер: " + cadNumber + "\r\nПредназначение: " + purpose.Item1
            + "\r\nПлощадь" + area;
            if (purpose.Item1 != null) str += "\r\nТип предназначения: " + purpose.Item1;
            str += "\r\nТип конструкции: " + type.Item1 + " " + type.Item2;
            if (adressType.Item1 != null) str += "Тип адреса: " + adressType.Item1 + "" + adressType.Item2;
            str += "\r\n\rАдрес: " + adress;

            if (okatoLoc != null) str += "\r\nOkato локации: " + okatoLoc;
            if (region.Item1 != null) str += "Регион локации: " + region.Item1 + "" + region.Item2;
            if (position != null) str += "\r\nПозиция локации: " + position;
            if (cost != 0) str += "\r\nЦена: " + cost;
            return str;
        }
    }
}
