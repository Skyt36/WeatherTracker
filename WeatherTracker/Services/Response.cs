using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.Services
{
    class Response
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }
}
