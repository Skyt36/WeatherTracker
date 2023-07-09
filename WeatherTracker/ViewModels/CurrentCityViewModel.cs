using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.ViewModels
{
    class CurrentCityViewModel
    {
        private int id_city;

        public CurrentCityViewModel()
        {
        }

        public CurrentCityViewModel(int id_city)
        {
            this.id_city = id_city;
        }
    }
}
