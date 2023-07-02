using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Configuration;
using WeatherTracker.Data;

namespace WeatherTracker.Services
{
    static class DB_address
    {
        static readonly string key = ConfigurationManager.AppSettings.Get("key");
        static readonly string url = ConfigurationManager.AppSettings.Get("url");
        static readonly HttpClient client = new HttpClient();
        public static async void addCity(string name)
        {
            //запрос к weatherapi
            string answer = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{url}key={key}&q={name}&aqi=no");
                response.EnsureSuccessStatusCode();
                answer = await response.Content.ReadAsStringAsync();
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Ошибка отправки запроса", e.Message);
            }
            if (answer == "")
            {
                return;
            }

            //перевод в JSON
            var weather = JsonConvert.DeserializeObject<Response>(answer);
            if ((weather?.location?.Name ?? "") == "")//не найден город
            {
                return;
            }

            Model db = new Model();

            //проверка на наличие города в базе
            var loc = (from location in db.City
                        where location.name == weather.location.Name
                        select location).FirstOrDefault();
            if (loc != null)//город уже добавлен
                return;

            //добавление горрода
            City city = new City();
            city.name = weather.location.Name;
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

        public static void DeleteCity(string name)
        {

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
