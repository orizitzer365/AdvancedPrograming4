using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace AdvancedPrograming4.Models
{
    public class Simulator
    {
        private TcpClient client;

        public Simulator(string ip,int port) {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);
        }
        public Locatoin GetLocation()
        {
            double lon, lat;
            StreamWriter writer;
            StreamReader reader;
            NetworkStream stream;
            using (stream = client.GetStream())
            using (writer = new StreamWriter(stream))
            using (reader = new StreamReader(stream)) { }
            writer.Write("get /position/longitude-deg");
            lon = double.Parse(reader.ReadLine());
            writer.Write("get /position/latitude-deg");
            lat = double.Parse(reader.ReadLine());
            stream.Close();
            return new Locatoin(lon,lat);
        }

        public FlightInfo GetInfo()
        {
            double thro, rud;
            StreamWriter writer;
            StreamReader reader;
            NetworkStream stream;
            using (stream = client.GetStream())
            using (writer = new StreamWriter(stream))
            using (reader = new StreamReader(stream)) { }
            writer.Write("get /controls/engines/current-engine/throttle");
            thro = double.Parse(reader.ReadLine());
            writer.Write("get /controls/flight/rudder");
            rud = double.Parse(reader.ReadLine());
            stream.Close();
            return new FlightInfo(thro,rud);
        }

        public void Close() {
            client.Close();
        }
    }
}