using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using weatherApi.Data;
using weatherApi.DTOs;
using weatherApi.Interfaces;
using weatherApi.Models;

namespace weatherApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/")]
    public class MeteorologicalController : ControllerBase
    {
        private MeteorologicalContext meteorologicalContext;
        private IMeteorologicalService meteorologicalService;
        private IMapper mapper;
        public MeteorologicalController(MeteorologicalContext Context, IMeteorologicalService meteorologicalService, IMapper mapper)
        {
            meteorologicalContext = Context;
            this.meteorologicalService = meteorologicalService;
            this.mapper = mapper;
        }

        [HttpGet("FindAll")]
        public IActionResult GetAllRegisters()
        {
            IEnumerable<MeteorologicalModel> meteorologicalList = meteorologicalService.ListAll();
            return meteorologicalList.Any() ? Ok(meteorologicalList) : NotFound("Meteorological Data not Found");
        }

        [HttpGet("FindToday/{city}")]
        public IActionResult GetRegisterToday(string city)
        {
            var response = meteorologicalService.FindByCityToday(city);
            return response is null ? NotFound("Meteorological Data not Found, choose another city") : Ok(response);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetById(Guid id)
        {
            var response = meteorologicalService.FindByID(id);
            return response is null ? NotFound("Meteorological Data not Found") : Ok(response);
        }

        [HttpGet("city/{city}")]
        public IActionResult GetAllByCity(string city)
        {
            IEnumerable<MeteorologicalModel> response = meteorologicalService.FindByCity(city);
            return response.Any() ? Ok(response) : NotFound("Meteorological Data not Found, choose another city");
        }

        [HttpGet("next7/{city}")]
        public IActionResult GetNextSevenDaysByCity(string city)
        {
            IEnumerable<MeteorologicalModel> response = meteorologicalService.ListNextSeven(city);
            return response.Any() ? Ok(response) : NotFound("Meteorological Data not Found, choose another city");
        }

        [HttpPost("post/")]
        public IActionResult Post([FromBody] MeteorologicalDTO meteorologicalDTO)
        {
            MeteorologicalModel weather = mapper.Map<MeteorologicalModel>(meteorologicalDTO);
            var createdResource = meteorologicalService.AddWeather(weather);
            return createdResource is null ? 
                Conflict("Request declined: You can only have one record per day for each city.") :
                CreatedAtAction(nameof(GetById), new { id = createdResource.Id }, createdResource);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Put(Guid id, [FromBody] MeteorologicalDTO meteorologicalDTO)
        {
            MeteorologicalModel weather = mapper.Map<MeteorologicalModel>(meteorologicalDTO);
            var response = meteorologicalService.EditWeather(id, weather);
            return response is not null ? Ok(response) : NotFound("Meteorological Data not Found, choose another ID");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
           var response = meteorologicalService.DeleteWeather(id);
           return response is not null ? Ok(response) : NotFound("ID not Found, choose another ID");
        }

    }
}
