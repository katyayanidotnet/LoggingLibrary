using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Business;
using Sample.Domain;

namespace Sample.Api1.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBusinessLogic _businessLogic;
        private static readonly HttpClient client = new HttpClient();

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBusinessLogic businessLogic)
        {
            _logger = logger;
            _businessLogic = businessLogic;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Hello from WeatherForecastController");
            var sums = this._businessLogic.Summaries();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = sums[rng.Next(sums.Length)]
            })
            .ToArray();
        }

        [Route("GetWeatherForecastFromService")]
        [HttpGet()]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastFromService()
        {
            _logger.LogInformation("Hello from GetWeatherForecastFromService");
            var stringTask = client.GetStringAsync("https://localhost:6003/weatherforecast");
            var msg = await stringTask;
            var sums = this._businessLogic.Summaries();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = sums[rng.Next(sums.Length)]
            })
            .ToArray();
        }

    }
}
