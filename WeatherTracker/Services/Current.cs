using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.Services
{
    class Current
    {
        public string Last_updated { get; set; }
        public float Temp_c { get; set; }
        public float Wind_mph { get; set; }
        public float Pressure_in { get; set; }
        public int Humidity { get; set; }
    }
}
