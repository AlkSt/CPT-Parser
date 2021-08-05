using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser.Models
{
    class Construction
    {
        public string cadNumber;
        public (string, string) purpose;
        public (string, string) type;
        public Adress adress;
        public override string ToString()
        {
            string str = "Кадастровый номер: " + cadNumber 
                + "\r\nПредназначение: " + purpose.Item1
                + "\r\nТип конструкции: " + type.Item1 + " " + type.Item2
                + "\r\n\rАдрес: " + adress;
            return str;
        }
    }
}
