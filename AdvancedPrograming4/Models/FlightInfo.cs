using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedPrograming4.Models
{
    public class FlightInfo
    {
        public double Throttle
        {
            get;
            set;
        }
        public double Rudder
        {
            get;
            set;
        }
        public FlightInfo(double throttle, double rudder) {
            Throttle = throttle;
            Rudder = rudder;
        }
        public override string ToString()
        {
            return Throttle.ToString() + "," + Rudder.ToString();
        }
    }
}