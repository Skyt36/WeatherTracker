using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace WeatherTracker.Models
{
    static class DB_address
    {
        const string key = "e3a47ea54a9d49799c4112128231906";
        public static async void addCity(string name)
        {
            WebRequest request = WebRequest.Create($"http://api.weatherapi.com/v1/current.json?key={key}&q={name}&aqi=no");
            request.Method = "POST";
            request.ContentType = "application/x-www-urlencoded";
            WebResponse response = await request.GetResponseAsync();
            string answer = "";
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();
            var weather = JsonConvert.DeserializeObject<Response>(answer);
            if ((weather?.location?.name ?? "") == "")//не найден город
            {
                return;
            }
            Model db = new Model();
            var loc = (from location in db.City
                        where location.name == weather.location.name
                        select location).FirstOrDefault();
            if (loc != null)//город уже добавлен
                return;
            City city = new City();
            city.name = weather.location.name;
            city.actual = false;
            var id = (from T in db.City
                      select T.id_city).FirstOrDefault();
            if (id == 0)
            {
                city.id_city = 1;
            }
            else
            {
                city.id_city = (from T in db.City
                                select T.id_city).Max() + 1;
            }
            db.City.Add(city);
            db.SaveChanges();
        }
        public static ICollection<City> GetAllCities()
        {
            Model db = new Model();
            ICollection<City> items = (from T in db.City
                                       select T).ToList();
            return items;
        }
    }
}
