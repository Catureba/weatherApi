using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherApi.Data;
using weatherApi.Data.Repository;
using weatherApi.DTOs;
using weatherApi.Interfaces;
using weatherApi.Models;
using weatherApi.Services;
using Xunit;

namespace WeatherApi.Tests.Services
{
    public class MeteorologicalServiceTests
    {
        private MeteorologicalService _meteorologicalService;
        private Mock<IMeteorologicalRepository> _meteorologicalRepository;

        public MeteorologicalServiceTests()
        {
            _meteorologicalRepository = new Mock<IMeteorologicalRepository>();
            _meteorologicalService = new MeteorologicalService(_meteorologicalRepository.Object);
        }
        

        private MeteorologicalModel meteorologicalModel1 = new MeteorologicalModel()
        {
            Id = Guid.NewGuid(),
            City = "Salvador",
            Date = DateTime.Now,
            Max_temperature = 10,
            Min_temperature = 10,
            Weather = MeteorologicalModel.WeatherType.SUNNY
        };

        private MeteorologicalModel meteorologicalModel2 = new MeteorologicalModel()
        {
            Id = Guid.NewGuid(),
            City = "Porto Alegre",
            Date = DateTime.Now,
            Max_temperature = 15,
            Min_temperature = 15,
            Weather = MeteorologicalModel.WeatherType.SHAKESPEARE
        };

        private MeteorologicalDTO meteorologicalDTO = new MeteorologicalDTO()
        {
            City = "SalvadorDTO",
            Date = DateTime.Now,
            Max_temperature = 20,
            Min_temperature = 20,
            Weather = (MeteorologicalDTO.WeatherType)MeteorologicalModel.WeatherType.TROPICAIS
        };

        

        [Fact]
        public void postMeteorologicalTest()
        {
            MeteorologicalModel result = _meteorologicalService.AddMeteorologicalRegister(meteorologicalModel1);
            
            Assert.NotNull(result);
            Assert.Equal(meteorologicalModel1, result);
        }

        [Fact]
        public void getMeteorologicalTest()
        {
            List<MeteorologicalModel> listMeteorological = new List<MeteorologicalModel>();
            listMeteorological.Add(meteorologicalModel1);
            listMeteorological.Add(meteorologicalModel2);

            _meteorologicalRepository.Setup(r => (r.ListAll())).Returns(listMeteorological);

            List<MeteorologicalModel> result = _meteorologicalService.ListAll();

            Assert.NotNull(result);
            Assert.Equal(listMeteorological, result);
        }
    }
}
