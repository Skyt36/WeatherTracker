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
using System.Globalization;

namespace WeatherTracker.Services
{
    public static class DB_address
    {
        static readonly string key = ConfigurationManager.AppSettings.Get("key");
        static readonly string url = ConfigurationManager.AppSettings.Get("url");
        static readonly HttpClient client = new HttpClient();
        public static async void AddCity(string name)
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

            DbModel db = new DbModel();

            //проверка на наличие города в базе
            var loc = (from location in db.City
                        where location.name == weather.location.Name
                        select location).FirstOrDefault();
            if (loc != null)//город уже добавлен
                return;

            //добавление горрода
            db.Database.ExecuteSqlCommand($"INSERT INTO City (name, actual) VALUES ('{weather.location.Name}', '0')");
            db.SaveChanges();
        }
        public static async void UpdateWeather()
        {
            DbModel db = new DbModel();
            var cities = (from city in db.City
                         where city.actual
                         select city);
            string weathers = "";
            foreach(City city in cities)
            {
                string answer = "";
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{url}key={key}&q={city.name}&aqi=no");
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
                var dateTimes = (from weatherTable in db.Weather
                                      where weatherTable.id_city == city.id_city
                                      select weatherTable.last_update);
                DateTime dateTime;
                if (dateTimes.Count() != 0)
                    dateTime = dateTimes.Max();
                else dateTime = DateTime.MinValue;
                if(DateTime.TryParse(weather.current.Last_updated, out DateTime currentDataTime))
                {
                    if(currentDataTime != dateTime)
                    {
                        string temp = weather.current.Last_updated.Substring(8, 2);
                        weather.current.Last_updated = weather.current.Last_updated.Remove(8, 2);
                        weather.current.Last_updated = weather.current.Last_updated.Insert(8, weather.current.Last_updated.Substring(5, 2));
                        weather.current.Last_updated = weather.current.Last_updated.Remove(5, 2);
                        weather.current.Last_updated = weather.current.Last_updated.Insert(5, temp);
                        if (weathers == "")
                        {
                            weathers = $"({city.id_city}, '{weather.current.Last_updated}', {weather.current.Temp_c.ToString(new CultureInfo("en-US",false))}, {weather.current.Humidity}, {weather.current.Pressure_in.ToString(new CultureInfo("en-US", false))}, {weather.current.Wind_mph.ToString(new CultureInfo("en-US", false))})";
                        }
                        else
                        {
                            weathers += $", ({city.id_city}, '{weather.current.Last_updated}', {weather.current.Temp_c.ToString(new CultureInfo("en-US", false))}, {weather.current.Humidity}, {weather.current.Pressure_in.ToString(new CultureInfo("en-US", false))}, {weather.current.Wind_mph.ToString(new CultureInfo("en-US", false))})";
                        }
                    }
                }
            }
            if (weathers != "")
            {
                db.Database.ExecuteSqlCommand("insert into Weather (id_city, last_update,temp_c, humidity,pressure_in,wind_mph) values "
                    + weathers + ";");
            }
            db.SaveChanges();
        }

        public static void DeleteCity(string name)
        {
            DbModel db = new DbModel();
            db.Database.ExecuteSqlCommand($"DELETE FROM City WHERE City.name = '{name}'");
            db.SaveChanges();
        }
        public static void SetActual(string name,bool actual)
        {
            DbModel db = new DbModel();
            int t=db.Database.ExecuteSqlCommand($"UPDATE City set City.actual = '{actual}' where City.name = '{name}'");
            db.SaveChanges();
        }
        public static ICollection<City> GetAllCities()
        {
            DbModel db = new DbModel();
            ICollection<City> items = (from T in db.City
                                       select T).ToList();
            return items;
        }
    }
}
