using CPT_Parser.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser.Models
{
    [Serializable]
    public class Construction : CadastralObject
    {
        public Construction()
        {
            purpose = new SreializableKeyValue<string, string>();
            type = new SreializableKeyValue<string, string>();
        }
        public string cadNumber;
        public SreializableKeyValue<string, string> purpose;
        public SreializableKeyValue<string, string> type;
        public Adress adress;
        public override string ToString()
        {
            string str = "Постройка"
                + "\r\n\rКадастровый номер: " + cadNumber 
                + "\r\nПредназначение: " + purpose.Item1
                + "\r\nТип конструкции: " + type.Item1 + " " + type.Item2
                + "\r\n\rАдрес: " + adress;
            return str;
        }
    }
}
