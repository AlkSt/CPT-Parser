using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CPT_Parser.Models
{
  
    class Parcel
    {
        public (string, string) type;
        internal (string, string) subtype;
        internal string cadNumber;
        public string link;
        internal (string, string) category;
        internal (string, string) permittedUse;
        internal (string, string) area;
        internal Adress adress;
        public string mark;
        internal double cost;
        internal SpatialData spatial;

        public override string ToString()
        {
            string str = "Кадастровый номер : " + cadNumber;
            if (area.Item1 != "-") str += "\r\nЦена: " + area.Item1;
            if (area.Item1 != "-") str += "\r\nПогрешность " + area.Item2;

            if (adress != null) str += "\r\nПолный адрес: " + adress;
            if (mark != "-") str += "\r\nМетка: " + mark;
            if (cost != 0) str += "\r\nЦена: " + cost;
            if (spatial != null) str += "\r\nОрдинаты: " + spatial;
            return str;
        }
    }
}
