using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using weatherApi.Data;
using weatherApi.DTOs;
using weatherApi.Interfaces;
using weatherApi.Models;

namespace weatherApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/")]
    [EnableCors("WeatherFront")]
    public class MeteorologicalController : ControllerBase
    {
        private IMeteorologicalService meteorologicalService;
        private IMapper mapper;
        public MeteorologicalController(IMeteorologicalService meteorologicalService, IMapper mapper)
        {
            this.meteorologicalService = meteorologicalService;
            this.mapper = mapper;
        }

        [HttpGet("FindAll")]
        public IActionResult GetAllRegisters()
        {
            IEnumerable<MeteorologicalModel> meteorologicalList = meteorologicalService.ListAll();
            return meteorologicalList.Any() ? Ok(meteorologicalList) : NotFound("Meteorological Data not Found");
        }

        [HttpGet("listNextSevenDaysByCity/{city}")]
        public IActionResult GetNextSevenDaysByCity(string city)
        {
            IEnumerable<MeteorologicalModel> response = meteorologicalService.ListNextSeven(city);
            return response.Any() ? Ok(response) : NotFound("Meteorological Data not Found, choose another city");
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var response = meteorologicalService.FindByID(id);
            return response is null ? NotFound("Meteorological Data not Found") : Ok(response);
        }

        [HttpGet("listByCity/{city}")]
        public IActionResult GetAllByCity(string city)
        {
            IEnumerable<MeteorologicalModel> response = meteorologicalService.FindByCity(city);
            return response.Any() ? Ok(response) : NotFound("Meteorological Data not Found, choose another city");
        }

        [HttpGet("getRegisterByCityToday/{city}")]
        public IActionResult GetRegisterToday(string city)
        {
            var response = meteorologicalService.FindByCityToday(city);
            return response is null ? NotFound("Meteorological Data not Found, choose another city") : Ok(response);
        }

        [HttpPost("postRegisterMeteorological/")]
        public IActionResult Post([FromBody] MeteorologicalDTO meteorologicalDTO)
        {
            MeteorologicalModel weather = mapper.Map<MeteorologicalModel>(meteorologicalDTO);
            MeteorologicalModel? idCreatedResource = meteorologicalService.AddMeteorologicalRegister(weather);

            return idCreatedResource is null ? 
                Conflict("Request declined: You can only have one record per day for each city.") :
                CreatedAtAction(nameof(GetById), new { id = idCreatedResource.Id }, idCreatedResource);
        }

        [HttpPut("editRegisterMeteorologicalById/{id}")]
        public IActionResult Put(Guid id, [FromBody] MeteorologicalDTO meteorologicalDTO)
        {
            MeteorologicalModel registerMeteorological = mapper.Map<MeteorologicalModel>(meteorologicalDTO);
            meteorologicalService.EditMeteorologicalRegister(id, registerMeteorological);
            return Ok("Edited the Meteorological register!");
        }

        [HttpDelete("deleteRegisterMeteorologicalById/{id}")]
        public IActionResult Delete(Guid id)
        {
           meteorologicalService.DeleteMeteorologicalRegister(id);
           return Ok("Deleted the Meteorological register!");
        }

    }
}
