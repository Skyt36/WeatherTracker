using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.Models
{
    class Response
    {
        public class Location
        {
            public string name { get; set; }
        }
        public class Current
        {
            public string last_updated { get; set; }
            public float temp_c { get; set; }
            public float wind_mph { get; set; }
            public float pressure_in { get; set; }
            public int humidity { get; set; }
        }

        public Location location { get; set; }
        public Current current { get; set; }
    }
}
