using CPT_Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CPT_Parser
{
    class Parseable
    {
        static public (Dictionary<string, CadastralObject>, Dictionary<string, CadastralObject>, Dictionary<string, CadastralObject>, Dictionary<string, CadastralObject>, Dictionary<string, CadastralObject>) ParsingData()
        {
            var fileXML = "\\CPT_Parser\\CPT_Parser\\datakpt11.xml";
            XDocument docXML = XDocument.Load(fileXML); // загрузить XML
            var data = docXML.Element("extract_cadastral_plan_territory").Element("cadastral_blocks").Element("cadastral_block");
            var newdata = data.Element("cadastral_number").Value;
            var land = ParsingLand(data.Element("record_data").Element("base_data"));
            var construction = ParsingObjectRealty(data.Element("record_data").Element("base_data"));
            var r = ParsingSpatial(data.Element("spatial_data").Element("entity_spatial"));
            var spat = new Dictionary<string, CadastralObject> { { r.skId, r } };
            var bound = ParsingBound(data.Element("municipal_boundaries"));
            var zone = ParsingZone(data.Element("zones_and_territories_boundaries"));
            return (land, construction, spat, bound, zone);
        }

        static public Dictionary<string, CadastralObject> ParsingLand(XElement xdoc)
        {
            var resList = new Dictionary<string, CadastralObject>();
            foreach (var construction in xdoc.Element("land_records").Elements("land_record"))
            {
                var parcel = new Parcel();
                var dataSection = construction.Element("object").Element("common_data");
                parcel.type.Item1 = dataSection.Element("type").Element("code").Value;
                parcel.type.Item1 = dataSection.Element("type").Element("value").Value;
                parcel.cadNumber = dataSection.Element("cad_number").Value;

                if (construction.Element("object").Element("subtype") != null)
                {
                    parcel.subtype.Item1 = construction.Element("object").Element("subtype").Element("code").Value;
                    parcel.subtype.Item2 = construction.Element("object").Element("subtype").Element("value").Value;
                }

                parcel.link = construction.Element("cad_links") != null ?
                    construction.Element("cad_links").Element("common_land").Element("common_land_cad_number").Element("cad_number").Value : "-";

                if (construction.Element("params").Element("category") != null)
                {
                    parcel.category.Item1 = construction.Element("params").Element("category").Element("type").Element("code").Value;
                    parcel.category.Item2 = construction.Element("params").Element("category").Element("type").Element("value").Value;
                }

                if (construction.Element("params").Element("permitted_use") != null)
                {
                    var premUseSection = construction.Element("params").Element("permitted_use").Element("permitted_use_established");
                    parcel.permittedUse.Item1 = premUseSection.Element("by_document").Value;
                    parcel.permittedUse.Item2 = premUseSection.Element("land_use") != null ?
                        premUseSection.Element("land_use").Element("value").Value: "-";
                }

                parcel.area.Item1 = construction.Element("params").Element("area").Element("value").Value;
                parcel.area.Item2 = construction.Element("params").Element("area").Element("inaccuracy") != null ?
                    construction.Element("params").Element("area").Element("inaccuracy").Value : "-";


                var adrSection = construction.Element("address_location").Element("address");
                parcel.adress = ParsingAdress(adrSection);
                
                parcel.mark = construction.Element("address_location").Element("rel_position").Element("in_boundaries_mark").Value;

                parcel.cost= construction.Element("cost") != null ? double.Parse(construction.Element("cost").Element("value").Value.Replace('.',',')) : 0;

                if (construction.Element("contours_location") != null)
                {
                    var sptEl = construction.Element("contours_location").Element("contours").Element("contour").Element("entity_spatial");
                    parcel.spatial = ParsingSpatial(sptEl);
                }
                resList.Add(parcel.cadNumber, parcel);
            }
            return resList;
        }

        static public Dictionary<string, CadastralObject> ParsingObjectRealty(XElement xdoc)
        {
            var construstion = ParsingConsruction(xdoc);
            var build = ParsingBuild(xdoc);
            foreach (var item in build)
                construstion.Add(item.Key, item.Value);
            return construstion;
        }
        static public Dictionary<string, CadastralObject> ParsingBuild(XElement xdoc)
        {
            var resList = new Dictionary<string, CadastralObject>();
            foreach (var buildEl in xdoc.Element("build_records").Elements("build_record"))
            {
                var build = new Build();
                var dataSection = buildEl.Element("object").Element("common_data");
                build.type.Item1 = dataSection.Element("type").Element("code").Value;
                build.type.Item2 = dataSection.Element("type").Element("value").Value;
                build.cadNumber = dataSection.Element("cad_number").Value;

                build.area = double.Parse(buildEl.Element("params").Element("area").Value.Replace(".",","));//блок
                build.purpose.Item1 = buildEl.Element("params").Element("purpose").Element("code").Value;
                build.purpose.Item2 = buildEl.Element("params").Element("purpose").Element("value").Value;

                //тип адреса
                if (buildEl.Element("address_location").Element("address_type") != null)
                {
                    build.adressType.Item1 = buildEl.Element("address_location").Element("address_type").Element("code").Value;
                    build.adressType.Item2 = buildEl.Element("address_location").Element("address_type").Element("value").Value;
                }
                //адрес
                var adrSection = buildEl.Element("address_location").Element("address");
                build.adress = ParsingAdress(adrSection);

                //локация адреса
                var locationSection = buildEl.Element("address_location").Element("location");
                build.okatoLoc = locationSection.Element("okato").Value;

                build.region.Item1 = locationSection.Element("region").Element("code").Value;
                build.region.Item2 = locationSection.Element("region").Element("value").Value;

                build.position = locationSection.Element("position_description") != null ?
                     locationSection.Element("position_description").Value : "-";


                build.cost = buildEl.Element("cost") != null ? 
                    double.Parse(buildEl.Element("cost").Element("value").Value.Replace(".", ",")) : 0;

                resList.Add(build.cadNumber, build);
            }
            return resList;
        }
        static public Dictionary<string, CadastralObject> ParsingConsruction(XElement xdoc)
        {
            var resList = new Dictionary<string, CadastralObject>();
            foreach (var constructionEl in xdoc.Element("construction_records").Elements("construction_record"))
            {
                var construction = new Construction();
                var dataSection = constructionEl.Element("object").Element("common_data");
                construction.type.Item1 = dataSection.Element("type").Element("code").Value;
                construction.type.Item2 = dataSection.Element("type").Element("value").Value;
                construction.cadNumber = dataSection.Element("cad_number").Value;

                construction.purpose.Item2 = constructionEl.Element("params").Element("purpose").Value;
                var adrSection = constructionEl.Element("address_location").Element("address");
                construction.adress = ParsingAdress(adrSection);
                resList.Add(construction.cadNumber, construction);
            }
            return resList;
        }
        static public Dictionary<string, CadastralObject> ParsingZone(XElement xdoc)
        {
            var resList = new Dictionary<string, CadastralObject>();
            foreach (var zoneEl in xdoc.Elements("zones_and_territories_record"))
            {
                var zone = new Zone();
                zone.registrationDate = zoneEl.Element("record_info").Element("registration_date").Value;

                var xombSection = zoneEl.Element("b_object_zones_and_territories").Element("b_object");
                zone.registrationNumber = xombSection.Element("reg_numb_border").Value;
                zone.typeBound.Item1 = xombSection.Element("type_boundary").Element("code").Value;
                zone.typeBound.Item2 = xombSection.Element("type_boundary").Element("value").Value;

                zone.typeZone.Item1 = zoneEl.Element("b_object_zones_and_territories").Element("type_zone").Element("code").Value;
                zone.typeZone.Item2 = zoneEl.Element("b_object_zones_and_territories").Element("type_zone").Element("value").Value;

                zone.number = zoneEl.Element("b_object_zones_and_territories").Element("number") != null ?
                    zoneEl.Element("b_object_zones_and_territories").Element("number").Value : "-";

                var sptEl = zoneEl.Element("b_contours_location").Element("contours").Element("contour").Element("entity_spatial");
                zone.spatial = ParsingSpatial(sptEl);
                resList.Add(zone.registrationNumber, zone);
            }
            return resList;
        }

        static public Dictionary<string, CadastralObject> ParsingBound(XElement xdoc)
        {
            var resList = new Dictionary<string, CadastralObject>();
            foreach (var boundEl in xdoc.Elements("municipal_boundary_record"))
            {
                var bound = new Bound();
                bound.registrationDate = boundEl.Element("record_info").Element("registration_date").Value;

                var xombSection = boundEl.Element("b_object_municipal_boundary").Element("b_object");
                bound.registrationNumber = xombSection.Element("reg_numb_border").Value;
                bound.type.Item1 = xombSection.Element("type_boundary").Element("code").Value;
                bound.type.Item2 = xombSection.Element("type_boundary").Element("value").Value;

                var sptEl = boundEl.Element("b_contours_location").Element("contours").Element("contour").Element("entity_spatial");
                bound.spatial = ParsingSpatial(sptEl);
                resList.Add(bound.registrationNumber, bound);
            }
            return resList;
        }

        static public SpatialData ParsingSpatial(XElement xdoc)
        {

            var spatial = new SpatialData();
            spatial.skId = xdoc.Element("sk_id").Value;
            foreach (var spat in xdoc.Element("spatials_elements").Element("spatial_element").Element("ordinates").Elements("ordinate"))
            {
                var x = double.Parse(spat.Element("x").Value.Replace('.', ','));
                var y = double.Parse(spat.Element("y").Value.Replace('.', ','));
                var num = spat.Element("ord_nmb") != null ? int.Parse(spat.Element("ord_nmb").Value) : -1;
                
                var geoNum = spat.Element("num_geopoint") != null ? spat.Element("num_geopoint").Value : "-";
                var geoZcrep = spat.Element("geopoint_zacrep") != null ? spat.Element("geopoint_zacrep").Value : "-";
                string opredCode = "-", оpredVal = "-";
                if (spat.Element("geopoint_opred") != null)
                {
                    opredCode = (spat.Element("geopoint_opred").Element("code").Value);
                    оpredVal = (spat.Element("geopoint_opred").Element("value").Value);
                }
                var delta = spat.Element("delta_geopoint") != null ? double.Parse(spat.Element("delta_geopoint").Value.Replace('.', ',')) : 0;

                spatial.AddOrdinate(x, y, num, geoNum, geoZcrep, opredCode, оpredVal, delta);
            }
            return spatial;
        }
        static private Adress ParsingAdress(XElement adress)
        {
            var resAdress = new Adress();
            var levelSection = adress.Element("address_fias").Element("level_settlement");

            resAdress.okato = levelSection.Element("okato").Value;
            resAdress.kladr = levelSection.Element("kladr").Value;

            resAdress.region.Item1 = levelSection.Element("region").Element("code").Value;
            resAdress.region.Item2 = levelSection.Element("region").Element("value").Value;

            if (levelSection.Element("district") != null)
            {
                resAdress.district.Item1 = levelSection.Element("district").Element("type_district").Value;
                resAdress.district.Item2 = levelSection.Element("district").Element("name_district").Value;
            }

            if (levelSection.Element("locality") != null)
            {
                resAdress.locality.Item1 = levelSection.Element("locality").Element("type_locality").Value;
                resAdress.locality.Item2 = levelSection.Element("locality").Element("name_locality").Value;
            }

            if (adress.Element("address_fias").Element("street") != null)
            {
                resAdress.street.Item1 = adress.Element("address_fias").Element("street").Element("type_street").Value;
                resAdress.street.Item2 = adress.Element("address_fias").Element("street").Element("name_street").Value;
                if (adress.Element("address_fias").Element("level1") != null)
                    resAdress.lavel = adress.Element("address_fias").Element("level1").Element("name_level1").Value;
            }
            

            if (adress.Element("address_fias").Element("detailed_level") != null)
            {
                var detal = adress.Element("address_fias").Element("detailed_level");
                if (detal.Element("street") != null)
                {
                    resAdress.street.Item1 = detal.Element("street").Element("type_street").Value;
                    resAdress.street.Item2 = detal.Element("street").Element("name_street").Value;
                    resAdress.lavel = detal.Element("level1") != null ? detal.Element("level1").Element("name_level1").Value : "-";
                }
                resAdress.other = detal.Element("other") != null ? detal.Element("other").Value : "-";
            }
            resAdress.readableAddress = adress.Element("readable_address").Value;
            return resAdress;
        }
    }
}
