using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication3.Data;


namespace WebApplication3
{
    public class ThreadApi
    {
        public WeatherApi weather_thread(string city)
        {
            var myWeatherTask = getWeather(city);
            myWeatherTask.Wait();
            WeatherApi myWeather = myWeatherTask.Result;

            
            string dataTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            var contextOptions = new DbContextOptionsBuilder<WebApplication3Context>()
.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebApplication3Context-6c28e34d-e6cb-4189-9bb9-60ff82f2a08f;Trusted_Connection=True;MultipleActiveResultSets=true")
.Options;
            var context = new WebApplication3Context(contextOptions);

            var weath = new Models.weather();
            weath.name = myWeather.name;
            weath.temp = Math.Round(myWeather.main.temp - 273.15, 1);
            weath.datatime = dataTime;
            weath.description = myWeather.weather[0].description;
            context.Add(weath);
            context.SaveChanges();


            return myWeather;
        }

        public static async Task<WeatherApi> getWeather(string city)
        {
            string call = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=08ba5dd31d0f7c44acd5ee3c8d364099";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(call);
            WeatherApi result = JsonConvert.DeserializeObject<WeatherApi>(response);
            return result;
        }

    }
}
