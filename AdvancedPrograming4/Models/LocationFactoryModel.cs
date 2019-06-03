using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedPrograming4.Models
{

    enum FactoryState {FROM_SIMULATOR , FROM_FILE , FROM_SIMULATOR_AND_SAVE , NOTHING }

    public class LocationFactoryModel
    {
        private FactoryState state;
        private Simulator simulator;
        private System.IO.StreamWriter writer;
        private System.IO.StreamReader reader;
        private static LocationFactoryModel instance;
        private static LocationFactoryModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocationFactoryModel(); 
                }
                switch (instance.state)
                {
                    case FactoryState.FROM_FILE:
                        {
                            instance.reader.Close();
                            break;
                        }
                    case FactoryState.FROM_SIMULATOR: {
                            instance.simulator.Close();
                            break;
                        }
                    case FactoryState.FROM_SIMULATOR_AND_SAVE:
                        {
                            instance.simulator.Close();
                            instance.writer.Flush();
                            instance.writer.Close();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                return instance;
            }
        }

        private LocationFactoryModel()
        {
            state = FactoryState.NOTHING;
        }

        public static LocationFactoryModel GetFromSimulatorFactory(string ip,int port)
        {
            LocationFactoryModel lfm = LocationFactoryModel.Instance;
            lfm.state = FactoryState.FROM_SIMULATOR;
            lfm.simulator = new Simulator(ip, port);
            return lfm;
        }

        public static LocationFactoryModel GetFromSimulatorAndSaveFactory(string ip, int port,string file)
        {
            LocationFactoryModel lfm = LocationFactoryModel.Instance;
            lfm.state = FactoryState.FROM_SIMULATOR_AND_SAVE;
            lfm.simulator = new Simulator(ip, port);
            lfm.writer = new System.IO.StreamWriter(file);
            return lfm;
        }

        public static LocationFactoryModel GetFromFile(string file)
        {
            LocationFactoryModel lfm = LocationFactoryModel.Instance;
            lfm.state = FactoryState.FROM_FILE;
            lfm.reader = new System.IO.StreamReader(file);
            return lfm;
        }

        private void Write(Locatoin loc,FlightInfo info)
        {
            writer.WriteLine("{0},{1}", loc, info);
        }

        private Locatoin Read()
        {
            if (reader.EndOfStream) return null;
            string str = reader.ReadLine();
            String[] substrings = str.Split(',');
            return new Locatoin(double.Parse(substrings[0]), double.Parse(substrings[1]));
        }

        public Locatoin GetNext()
        {
            switch (state)
            {
                case FactoryState.FROM_FILE:
                    {
                        return Read();
                    }
                case FactoryState.FROM_SIMULATOR:
                    {
                        return simulator.GetLocation();
                    }
                case FactoryState.FROM_SIMULATOR_AND_SAVE:
                    {
                        Locatoin loc = simulator.GetLocation();
                        FlightInfo info = simulator.GetInfo();
                        Write(loc, info);
                        return loc;
                    }
                default:
                    {
                        break;
                    }
            }
            return null;
        }
    }
}