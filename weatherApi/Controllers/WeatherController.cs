using Microsoft.AspNetCore.Mvc;
using weatherApi.Data;
using weatherApi.Models;

namespace weatherApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WeatherController : Controller
    {
        private WeatherContext weatherContext;
        public WeatherController(WeatherContext Context)
        {
            weatherContext = Context;
        }

        [HttpGet]
        public IEnumerable<WeatherModel> GetAllRegisters()
        {
            return weatherContext.Weathers;
        }

        [HttpPost]
        public void Post([FromBody] WeatherModel model)
        {
            
            weatherContext.Weathers.Add(model);
            weatherContext.SaveChanges();
            Console.WriteLine(model.City);
        }

        [HttpPut("/api/Weather/{id}")]
        public void Put(int id, [FromBody] WeatherModel model)
        {
            var old = weatherContext.Weathers.FirstOrDefault(x => x.Id == id);

            if ( old != null)
            {
                old.City = model.City;
                old.Data = model.Data;
                old.Max_temperature = model.Max_temperature;
                old.Min_temperature = model.Min_temperature;
                old.Weater = model.Weater;
                weatherContext.SaveChanges();
            }

        }

        [HttpDelete("/api/Weather/{id}")]
        public void Delete(int id)
        {
            var element = weatherContext.Weathers.FirstOrDefault(x => x.Id == id);

            if (element != null)
            {
                weatherContext.Weathers.Remove(element);
                weatherContext.SaveChanges();
            }

        }

    }
}
