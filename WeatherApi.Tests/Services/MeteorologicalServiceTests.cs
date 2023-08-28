using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private Mock<IMapper> _mapper;

        public MeteorologicalServiceTests()
        {
            _meteorologicalRepository = new Mock<IMeteorologicalRepository>();
            _mapper = new Mock<IMapper>();
            _meteorologicalService = new MeteorologicalService(_meteorologicalRepository.Object, _mapper.Object);
        }
        

        private MeteorologicalModel meteorologicalModel1 = new MeteorologicalModel
        {
            Id = Guid.NewGuid(),
            City = "salvador",
            Date = DateTime.Now,
            Max_temperature = 10,
            Min_temperature = 10,
            Weather = MeteorologicalModel.WeatherType.SUNNY
        };

        private MeteorologicalModel meteorologicalModel2 = new MeteorologicalModel
        {
            Id = Guid.NewGuid(),
            City = "porto alegre",
            Date = DateTime.Now,
            Max_temperature = 15,
            Min_temperature = 15,
            Weather = MeteorologicalModel.WeatherType.SHAKESPEARE
        };


        private MeteorologicalDTO meteorologicalDTO = new MeteorologicalDTO
        {
            City = "salvadordto",
            Date = DateTime.Now,
            Max_temperature = 20,
            Min_temperature = 20,
            Weather = (MeteorologicalDTO.WeatherType)MeteorologicalModel.WeatherType.RAINY
        };

        

        [Fact]
        public void successfulPostMeteorologicalTest()
        {
            //ARRANGE:
            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel>();
            meteorologicalListByCity.Add(meteorologicalModel2);
            _meteorologicalRepository.Setup(repository => (repository.FindByCity(meteorologicalModel1.City))).Returns(meteorologicalListByCity);
            _meteorologicalRepository.Setup(repository => (repository.AddMeteorologicalRegister(meteorologicalModel1))).Returns(meteorologicalModel1);

            //ACT:
            MeteorologicalModel? result = _meteorologicalService.AddMeteorologicalRegister(meteorologicalModel1);

            //ASSERT:
            Assert.NotNull(result);
            Assert.Equal(meteorologicalModel1, result);
        }

        [Fact]
        public void badPostMeteorologicalTest()
        {
            //ARRANGE:
            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel>();
            meteorologicalListByCity.Add(meteorologicalModel1);
            _meteorologicalRepository.Setup(repository => (repository.FindByCity(meteorologicalModel1.City))).Returns(meteorologicalListByCity);
            _meteorologicalRepository.Setup(repository => (repository.AddMeteorologicalRegister(meteorologicalModel1))).Returns(meteorologicalModel1);

            //ACT:
            MeteorologicalModel? result = _meteorologicalService.AddMeteorologicalRegister(meteorologicalModel1);

            //ASSERT:
            Assert.Null(result);
        }

        [Fact]
        public void editMeteorologicalTest()
        {
            //ARRANGE:
            MeteorologicalModel meteorologicalDataEdited = new MeteorologicalModel
            {
                Id = meteorologicalModel1.Id,
                City = meteorologicalModel2.City,
                Date = meteorologicalModel2.Date,
                Max_temperature = meteorologicalModel2.Max_temperature,
                Min_temperature = meteorologicalModel2.Min_temperature,
                Weather = meteorologicalModel2.Weather
            };
            _meteorologicalRepository.Setup(repository => (repository.FindByID(meteorologicalModel1.Id))).Returns(meteorologicalModel1);
            _meteorologicalRepository.Setup(repository => (repository.EditMeteorologicalRegister(meteorologicalModel2))).Returns(meteorologicalDataEdited);


            //ACT:
            MeteorologicalModel? result = _meteorologicalService.EditMeteorologicalRegister(meteorologicalModel1.Id, meteorologicalModel2);

            //ASSERT:
            Assert.NotNull(result);
            Assert.Equal(meteorologicalDataEdited.City, result.City);
            Assert.Equal(meteorologicalDataEdited.Date, result.Date);
            Assert.Equal(meteorologicalDataEdited.Id, result.Id);
            Assert.Equal(meteorologicalDataEdited.Max_temperature, result.Max_temperature);
            Assert.Equal(meteorologicalDataEdited.Min_temperature, result.Min_temperature);
        }

        [Fact]
        public void getMeteorologicalTest()
        {
            List<MeteorologicalModel> listMeteorological = new List<MeteorologicalModel>();
            listMeteorological.Add(meteorologicalModel1);
            listMeteorological.Add(meteorologicalModel2);

            _meteorologicalRepository.Setup(repository => (repository.ListAll())).Returns(listMeteorological);

            List<MeteorologicalModel> result = _meteorologicalService.ListAll();

            Assert.NotNull(result);
            Assert.Equal(listMeteorological, result);
        }
    }
}
