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
            
        }
        Dictionary<string, Parcel> ParcelElenents;
        Dictionary<string, Construction> ConstructionElenents;
        SpatialData SpatialElenents;
        Dictionary<string, Bound> BoundElenents;
        Dictionary<string, Zone> ZoneElenents;

        public void UploadData()
        {
            (ParcelElenents, ConstructionElenents,  SpatialElenents, BoundElenents, ZoneElenents) = Parseable.ParsingData();
        }

        public Dictionary<string, Parcel> getParcel() => ParcelElenents;
        public Dictionary<string, Construction> getObjectRealty() => ConstructionElenents;
        public SpatialData getSpatial() => SpatialElenents;
        public Dictionary<string, Bound> getBound() => BoundElenents;
        public Dictionary<string, Zone> getZone() => ZoneElenents;
    }
}
