using CPT_Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser
{
    class DataCadastralSet
    {
        public DataCadastralSet() { }
        public Dictionary<string, CadastralObject> ParcelElenents { get; set; }
        public Dictionary<string, CadastralObject> ObjectRealtyElenents { get; set; }
        public Dictionary<string, CadastralObject> SpatialElenents { get; set; }
        public Dictionary<string, CadastralObject> BoundElenents { get; set; }
        public Dictionary<string, CadastralObject> ZoneElenents { get; set; }


        public CadastralObject GetObjectById(string id)
        {
            if (ParcelElenents.ContainsKey(id))
                return ParcelElenents[id];
            else if (ObjectRealtyElenents.ContainsKey(id))
                return ObjectRealtyElenents[id];
            else if (SpatialElenents.ContainsKey(id))
                return SpatialElenents[id];
            else if (BoundElenents.ContainsKey(id))
                return BoundElenents[id];
            else if (ZoneElenents.ContainsKey(id))
                return ZoneElenents[id];
            else return null;
        }
    }
}
