using CPT_Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser
{
    class Data
    {
        public Data()
        {
            //можно записать в конструктор по отдельности
        }
        Dictionary<string, CadastralObject> ParcelElenents;
        Dictionary<string, CadastralObject> ConstructionElenents;
        Dictionary<string, CadastralObject> SpatialElenents;
        Dictionary<string, CadastralObject> BoundElenents;
        Dictionary<string, CadastralObject> ZoneElenents;

        public void UploadData()
        {
            (ParcelElenents, ConstructionElenents,  SpatialElenents, BoundElenents, ZoneElenents) = Parseable.ParsingData();
        }

        public Dictionary<string, CadastralObject> getParcel() => ParcelElenents;
        public Dictionary<string, CadastralObject> getObjectRealty() => ConstructionElenents;
        public Dictionary<string, CadastralObject> getSpatial() => SpatialElenents;
        public Dictionary<string, CadastralObject> getBound() => BoundElenents;
        public Dictionary<string, CadastralObject> getZone() => ZoneElenents;

        public CadastralObject getObject(string id)
        {
            if (ParcelElenents.ContainsKey(id))
                return ParcelElenents[id];
            else if (ConstructionElenents.ContainsKey(id))
                return ConstructionElenents[id];
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
