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
            Date = new DateTime(2022, 12, 30),
            Max_temperature = 10,
            Min_temperature = 10,  
            Weather_day = "SUNNY",
            Weather_night = "RAINY",
            Humidity = 12,
            Wind_speed = 10,
            Precipitation = 10,
        };

        private MeteorologicalModel meteorologicalModel2 = new MeteorologicalModel
        {
            Id = Guid.NewGuid(),
            City = "porto alegre",
            Date = new DateTime(2022, 12, 30),
            Max_temperature = 15,
            Min_temperature = 15,
            Weather_day = "SUNNY",
            Weather_night = "RAINY",
            Humidity = 12,
            Wind_speed = 10,
            Precipitation = 10,
        };


        private MeteorologicalDTO meteorologicalDTO = new MeteorologicalDTO
        {
            City = "salvadordto",
            Date = new DateTime(2022, 12, 30),
            Max_temperature = 20,
            Min_temperature = 20,
            Weather_day = "SUNNY",
            Weather_night = "RAINY",
            Humidity = 12,
            Wind_speed = 10,
            Precipitation = 10,
        };



        [Fact]
        public void ListWithPagination_ReturnsAllRegisters_WhenCityIsEmpty()
        {
            // Arrange
            List<MeteorologicalModel> meteorologicalList = new List<MeteorologicalModel> { meteorologicalModel2, meteorologicalModel1 };
            _meteorologicalRepository.Setup(repository => (repository.ListAll())).Returns(meteorologicalList);

            // Act
            var resultWithOutCity = _meteorologicalService.ListWithPagination(0);

            // Assert
            Assert.NotNull(resultWithOutCity);
        }

        [Fact]
        public void ListWithPagination_ReturnsFilteredRegisters_WhenCityIsProvided()
        {
            // Arrange
            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel> { meteorologicalModel1 };
            _meteorologicalRepository.Setup(repository => (repository.FindByCity(meteorologicalModel1.City))).Returns(meteorologicalListByCity);

            // Act
            var resultWithCity = _meteorologicalService.ListWithPagination(0, meteorologicalModel1.City);

            // Assert
            Assert.NotNull(resultWithCity);
        }

        [Fact]
        public void FinfByCity_ReturnsFilteredRegisters()
        {
            // Arrange
            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel> { meteorologicalModel1 };
            _meteorologicalRepository.Setup(repository => (repository.FindByCity(meteorologicalModel1.City))).Returns(meteorologicalListByCity);

            // Act
            var result = _meteorologicalService.FindByCity(meteorologicalModel1.City);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void FindByCityToday_ReturnsFilteredRegisterToday()
        {
            // Arrange
            MeteorologicalModel meteorologicalRegisterToday = meteorologicalModel1;
            _meteorologicalRepository.Setup(repository => (repository.FindByCityToday(meteorologicalModel1.City))).Returns(meteorologicalRegisterToday);

            // Act
            var result = _meteorologicalService.FindByCityToday(meteorologicalModel1.City);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, meteorologicalModel1);
        }

        public void FindById_Returns_RegisterById()
        {
            // Arrange
            MeteorologicalModel meteorologicalRegister = meteorologicalModel1;
            _meteorologicalRepository.Setup(repository => (repository.FindByID(meteorologicalModel1.Id))).Returns(meteorologicalRegister);

            // Act
            var result = _meteorologicalService.FindByID(meteorologicalModel1.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, meteorologicalModel1);
        }

        [Fact]
        public void successfulPostMeteorologicalTest()
        {
            //ARRANGE:
            MeteorologicalModel meteorologicalPost = new MeteorologicalModel
            {
                Id = Guid.NewGuid(),
                City = "salvador",
                Date = new DateTime(2022, 12, 30),
                Max_temperature = 33,
                Min_temperature = 32,
                Weather_day = "SUNNY",
                Weather_night = "RAINY",
                Humidity = 12,
                Wind_speed = 10,
                Precipitation = 10,

            };

            MeteorologicalModel meteorologicalOtherCity = new MeteorologicalModel
            {
                Id = Guid.NewGuid(),
                City = "salvador",
                Date = new DateTime(2023, 1, 1),
                Max_temperature = 22,
                Min_temperature = 11,
                Weather_day = "SUNNY",
                Weather_night = "RAINY",
                Humidity = 12,
                Wind_speed = 10,
                Precipitation = 10,
            };

            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel>();
            meteorologicalListByCity.Add(meteorologicalOtherCity);
            _meteorologicalRepository.Setup(repository => (repository.FindByCity(meteorologicalPost.City))).Returns(meteorologicalListByCity);
            _meteorologicalRepository.Setup(repository => (repository.AddMeteorologicalRegister(meteorologicalPost))).Returns(meteorologicalPost);

            //ACT:
            MeteorologicalModel? result = _meteorologicalService.AddMeteorologicalRegister(meteorologicalPost);

            //ASSERT:
            Assert.NotNull(result);
            Assert.Equal(meteorologicalPost, result);
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
                Weather_day = meteorologicalModel2.Weather_day,
                Weather_night = meteorologicalModel2.Weather_night,
                Precipitation = meteorologicalModel2.Precipitation,
                Wind_speed = meteorologicalModel2.Wind_speed,
                Humidity = meteorologicalModel2.Humidity
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
            //ARRANGE
            List<MeteorologicalModel> listMeteorological = new List<MeteorologicalModel>();
            listMeteorological.Add(meteorologicalModel1);
            listMeteorological.Add(meteorologicalModel2);

            _meteorologicalRepository.Setup(repository => (repository.ListAll())).Returns(listMeteorological);

            //ACT
            List<MeteorologicalModel> result = _meteorologicalService.ListAll();
            
            //ASSERT
            Assert.NotNull(result);
            Assert.Equal(listMeteorological, result);
        }

        [Fact]
        public void getSevenNextDaysMeteorologicalTest()
        {
            //ARRANGE
            List<MeteorologicalModel> listByCity = new List<MeteorologicalModel>();
            MeteorologicalModel meteorologicalRegister = new MeteorologicalModel
            {
                Id = Guid.NewGuid(),
                City = "candeias",
                Date = DateTime.Now,
                Max_temperature = 44,
                Min_temperature = 37,
                Weather_day = "SUNNY",
                Weather_night = "RAINY",
                Humidity = 12,
                Wind_speed = 10,
                Precipitation = 10,
            };

            for (int i = 0; i < 7; i++)
            {
                meteorologicalRegister.Date = meteorologicalRegister.Date.AddDays(1);
                listByCity.Add(meteorologicalRegister);
            }
            _meteorologicalRepository.Setup(repository => (repository.ListNextSeven("candeias"))).Returns(listByCity);
            //ACT
            var result = _meteorologicalService.ListNextSeven("candeias");

            //ASSERT
            Assert.NotNull(result);
            Assert.Equal(listByCity, result);
        }
    }
}
