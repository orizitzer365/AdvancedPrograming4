using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace AdvancedPrograming4.Models
{
    public class Locatoin
    {
        public double Lat{get; set;}
        public double Lon { get; set; }

        public Locatoin( double lon, double lat)
        {
            Lat = lat;
            Lon = lon;
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Location");
            writer.WriteElementString("Lon", this.Lon.ToString());
            writer.WriteElementString("Lat", this.Lat.ToString());
            writer.WriteEndElement();
        }
    }
}