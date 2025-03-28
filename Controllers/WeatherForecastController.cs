using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var weatherForecasts = new List<WeatherForecast>();
            for (int i = 0; i < 5; i++)
            {
                weatherForecasts.Add(new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                });
            }
            return weatherForecasts;
        }


        [HttpGet("{numDays}")]
        public IEnumerable<WeatherForecast> Get(int numDays)
        {
            var rng = new Random();
            var weatherForecasts = new List<WeatherForecast>();
            for (int i = 0; i < numDays; i++)
            {
                weatherForecasts.Add(new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                });
            }
            return weatherForecasts;
        }
    }
}