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
        private IPEndPoint ep;
        public Simulator(string ip,int port) {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            
        }
        public Locatoin GetLocation()
        {
            client = new TcpClient();
            client.Connect(ep);
            //client.Connect(ep);
            double lon, lat;
            StreamWriter writer;
            StreamReader reader;
            NetworkStream stream;
            using (stream = client.GetStream())
            using (writer = new StreamWriter(stream))
            using (reader = new StreamReader(stream)) {
                writer.Write("get /position/longitude-deg\r\n");
                writer.Flush();
                string s = reader.ReadLine();
                s = s.Split('=')[1].Split(' ')[1].Split((char)39)[1];
                lon = double.Parse(s);
                writer.Write("get /position/latitude-deg\r\n");
                writer.Flush();
                s = reader.ReadLine();
                lat = double.Parse(s.Split('=')[1].Split(' ')[1].Split((char)39)[1]);
                stream.Close();
            }
            //client.Connect(ep);
            //stream.Close();
            Locatoin loc =  new Locatoin(lon, lat);
            client.Close();
            return loc;
        }

        public FlightInfo GetInfo()
        {
            client = new TcpClient();
            client.Connect(ep);
            double thro, rud;
            StreamWriter writer;
            StreamReader reader;
            NetworkStream stream;
            using (stream = client.GetStream())
            using (writer = new StreamWriter(stream))
            using (reader = new StreamReader(stream)) {
                writer.Write("get /controls/engines/current-engine/throttle\r\n");
                writer.Flush();
                string s = reader.ReadLine();
                s = s.Split('=')[1].Split(' ')[1].Split((char)39)[1];
                thro = double.Parse(s);
                writer.Write("get /controls/flight/rudder\r\n");
                writer.Flush();
                s = reader.ReadLine();
                s = s.Split('=')[1].Split(' ')[1].Split((char)39)[1];
                rud = double.Parse(s);
                stream.Close();
            }
            client.Close();

            return new FlightInfo(thro,rud);
        }

        public void Close() {
            //client.Close();
        }
    }
}