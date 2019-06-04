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
        
        private LocationFactoryModel model
        {
            set => Session["model"] = value;
            get => (LocationFactoryModel)Session["model"];
        }

        private bool IsIP(string str)
        {
            System.Net.IPAddress custResult;
            return System.Net.IPAddress.TryParse(str,out custResult);
        }

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult display(string ip_or_file, int port_or_refreshRate, int refreshRate=-1)
        {
            if (refreshRate != -1)
            {
                Session["time"] = refreshRate;
                Session["timeout"] = 0;
                model = LocationFactoryModel.GetFromSimulatorFactory(ip_or_file, port_or_refreshRate);
                return View();
            }
            else
            {
                Session["time"] = -1;
                Session["timeout"] = 0;
                if (IsIP(ip_or_file))
                {
                    model = LocationFactoryModel.GetFromSimulatorFactory(ip_or_file, port_or_refreshRate);
                }
                else
                {
                    Session["time"] = port_or_refreshRate;
                    model = LocationFactoryModel.GetFromFile(ip_or_file);
                }
                return View();
            }
            
        }

        public ActionResult save(string ip, int port, int refreshRate, int timeout, string fileName)
        {
            Session["time"] = refreshRate;
            Session["timeout"] = timeout;
            model = LocationFactoryModel.GetFromSimulatorAndSaveFactory(ip, port, fileName);
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
            Locatoin loc = model.GetNext();
            if (loc == null)
                loc = new Locatoin(-1, -1);
            return ToXml(loc);
        }
    }
}