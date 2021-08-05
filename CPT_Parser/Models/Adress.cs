using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser.Models
{
    class Adress
    {
        public Adress() { }
        public string okato;
        public string kladr;
        public (string, string) region;
        public (string, string) district;
        public (string, string) locality;
        public (string, string) street;
        public string lavel;
        public string other;
        public string readableAddress;

        public override string ToString()
        {
            string str = "Okato : " + okato + "\r\n\rKladr:" + kladr;

            if (region.Item2 != "-") str += "\r\nРегион: " + region.Item1 + " "+ region.Item2;
            if (district.Item2 != null) str += "\r\nРайон: " + district.Item1 + " " + district.Item2;

            if (locality.Item2 != null) str += "\r\nЛокация: " + locality.Item1 + " " + locality.Item2;
            if (street.Item2 != null) str += "\r\nУлица: " + street.Item1 + " " + street.Item2;
            if (lavel != null) str += "\r\nДом: д. " + lavel;
            if (other != null) str += "\r\nДополнительно: " + other;
            if (readableAddress != "-") str += "\r\nПолный адрес: " + readableAddress;
            return str;
        }
    }
}
