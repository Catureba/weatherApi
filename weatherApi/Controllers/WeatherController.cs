using Microsoft.AspNetCore.Mvc;
using weatherApi.Data;
using weatherApi.Interfaces;
using weatherApi.Models;

namespace weatherApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WeatherController : ControllerBase
    {
        private WeatherContext weatherContext;
        private IWeatherService weatherService;
        public WeatherController(WeatherContext Context, IWeatherService weatherService)
        {
            weatherContext = Context;
            this.weatherService = weatherService;
        }

        [HttpGet]
        public IEnumerable<WeatherModel> GetAllRegisters()
        {
            return weatherService.ListAll();
        }

        [HttpGet("/api/Weather/id/{id}")]
        public IActionResult GetById(Guid id)
        {
            var response = weatherService.FindByID(id);
            return Ok(response);
        }

        [HttpGet("/api/Weather/city/{city}")]
        public IEnumerable<WeatherModel>? GetAllByCity(string city)
        {
            return weatherService.FindByCity(city);
        }

        [HttpGet("/api/Weather/next7/{city}")]
        public IEnumerable<WeatherModel>? GetNextSevenDaysByCity(string city)
        {
            return weatherService.ListNextSeven(city);
        }

        [HttpPost]
        public WeatherModel? Post([FromBody] WeatherModel model)
        {
            return weatherService.AddWeather(model);
        }

        [HttpPut("/api/Weather/{id}")]
        public WeatherModel? Put(Guid id, [FromBody] WeatherModel model)
        {
            return weatherService.EditWeather(id, model);
        }

        [HttpDelete("/api/Weather/{id}")]
        public WeatherModel? Delete(Guid id)
        {
           return weatherService.DeleteWeather(id);
        }

    }
}
