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


        [HttpGet("listRegisters")]
        public async Task<IActionResult> ListAllRegisters(string? city = "", int skip = 0)
        {
            var allRegisters = await meteorologicalService.ListWithPagination(skip, city);
            return allRegisters.data.Any() ? Ok(allRegisters) : NotFound("Meteorological Data not Found");
        }


        [HttpGet("FindAll")]
        public async Task<IActionResult> GetAllRegisters()
        {
            var meteorologicalList = await meteorologicalService.ListAll();
            return meteorologicalList.Any() ? Ok(meteorologicalList) : NotFound("Meteorological Data not Found");
        }

        [HttpGet("listNextSevenDaysByCity/{city}")]
        public async Task<IActionResult> GetNextSevenDaysByCity(string city)
        {
            IEnumerable<MeteorologicalModel> response = await meteorologicalService.ListNextSeven(city);
            return response.Any() ? Ok(response) : NotFound("Meteorological Data not Found, choose another city");
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await meteorologicalService.FindByID(id);
            return response is null ? NotFound("Meteorological Data not Found") : Ok(response);
        }

        [HttpGet("listByCity/{city}")]
        public async Task<IActionResult> GetAllByCity(string city)
        {
            var response = await meteorologicalService.FindByCity(city);
            return response.Any() ? Ok(response) : NotFound("Meteorological Data not Found, choose another city");
        }

        [HttpGet("getRegisterByCityToday/{city}")]
        public async Task<IActionResult> GetRegisterToday(string city)
        {
            var response = await meteorologicalService.FindByCityToday(city);
            return response is null ? NotFound("Meteorological Data not Found, choose another city") : Ok(response);
        }

        [HttpPost("postRegisterMeteorological/")]
        public async Task<IActionResult> Post([FromBody] MeteorologicalDTO meteorologicalDTO)
        {
            MeteorologicalModel weather = mapper.Map<MeteorologicalModel>(meteorologicalDTO);
            MeteorologicalModel? idCreatedResource = await meteorologicalService.AddMeteorologicalRegister(weather);

            return idCreatedResource is null ? 
                Conflict("Request declined: You can only have one record per day for each city.") :
                CreatedAtAction(nameof(GetById), new { id = idCreatedResource.Id }, idCreatedResource);
        }

        [HttpPut("editRegisterMeteorologicalById/{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MeteorologicalDTO meteorologicalDTO)
        {
            MeteorologicalModel registerMeteorological = mapper.Map<MeteorologicalModel>(meteorologicalDTO);
            var response = await meteorologicalService.EditMeteorologicalRegister(id, registerMeteorological);
            return response is null? 
                NotFound():
                Ok(response);
        }

        [HttpDelete("deleteRegisterMeteorologicalById/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
           await meteorologicalService.DeleteMeteorologicalRegister(id);
           return Ok("Deleted the Meteorological register!");
        }

    }
}
