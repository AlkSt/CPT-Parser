using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CPT_Parser.Serialize;

namespace CPT_Parser.Models
{

    [Serializable]
    public class Parcel : CadastralObject
    {
        public Parcel()
        {
            type = new SreializableKeyValue<string, string>();
            subtype = new SreializableKeyValue<string, string>();
            category = new SreializableKeyValue<string, string>();
            permittedUse = new SreializableKeyValue<string, string>();
            area = new SreializableKeyValue<string, string>();

        }
        public SreializableKeyValue<string, string> type;
        public SreializableKeyValue<string, string> subtype;
        public string cadNumber;
        public string link;
        public SreializableKeyValue<string, string> category;
        public SreializableKeyValue<string, string> permittedUse;
        public SreializableKeyValue<string, string> area;
        public Adress adress;
        public string mark;
        public double cost;
        public SpatialData spatial;

        public override string ToString()
        {
            string str ="Участок"
                + "\r\n\rТип участка: " + type.Item1 + " " + type.Item2
                + "\r\nКадастровый номер : " + cadNumber;
            if (area.Item2 != "-") str += "\r\nПодвид участка: " + subtype.Item1 + " " + subtype.Item2;
            if (area.Item1 != "-") str += "\r\nПлощадь: " + area.Item1;
            if (area.Item2 != "-") str += "\r\nПогрешность: " + area.Item2;
            if (link != "-") str += "\r\nСсылки:  " + link;
            if (category.Item1 != null) str += "\r\nКатегория: " + category.Item1 + " " + category.Item2;
            if (permittedUse.Item1 != null) str += "\r\nРазрешено использование по документам : " + permittedUse.Item1;
            if (permittedUse.Item2 != null) str += "\r\nИспользование земли : " + permittedUse.Item2;
            if (adress != null) str += "\r\n\rПолный адрес: " + adress;
            if (mark != "-") str += "\r\nМетка: " + mark;
            if (cost != 0) str += "\r\nЦена: " + cost;
            if (spatial != null) str += "\r\n\r" + spatial;
            return str;
        }
    }
}
