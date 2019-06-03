using AdvancedPrograming4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace AdvancedPrograming4.Controllers
{
    public class DefaultController : Controller
    {
        private static Random rnd = new Random();

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult display(string ip , int port,int refreshRate=-1,int timeout=0,string fileName=null)
        {
            Session["time"] = refreshRate;
            Session["timeout"] = timeout;
            //set Model
            return View();
        }


        private string ToXml(Locatoin locatoin)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Locations");

            locatoin.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }

        [HttpPost]
        public string GetLocation()
        {

            var loc = new Locatoin(rnd.Next(-180,180),rnd.Next(-90,90));

            return ToXml(loc);
        }
    }
}