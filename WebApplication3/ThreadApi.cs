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

            Thread.Sleep(5000);

            
            string dataTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string toDisplay = city + " (" + dataTime + ")";
            toDisplay += ": " + (myWeather.main.temp - 273.15).ToString() + "°C";
            toDisplay += ": " + (myWeather.weather[0].description);

            var context = new WebApplication3Context(null);
            context.Add(new WebApplication3.Models.weather { name = myWeather.name, datatime = dataTime, temp = myWeather.main.temp, description = myWeather.weather[0].description });
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
