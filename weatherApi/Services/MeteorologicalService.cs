using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weatherApi.Data;
using weatherApi.Data.Repository;
using weatherApi.Interfaces;
using weatherApi.Models;

namespace weatherApi.Services
{
    public class MeteorologicalService : IMeteorologicalService
    {
        private IMeteorologicalRepository _meteorologicalRepository;
        private IMapper _mapper;
        public MeteorologicalService(IMeteorologicalRepository meteorologicalRepository, IMapper mapper) 
        {
            _meteorologicalRepository = meteorologicalRepository;
            _mapper = mapper;
        }

        public List<MeteorologicalModel> ListAll()
        {
            var meteorologicalList = _meteorologicalRepository.ListAll();
            return meteorologicalList;
        }
        public List<MeteorologicalModel> FindByCity(string city)
        {
            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel>();

            foreach (MeteorologicalModel weather in _meteorologicalRepository.ListAll())
            {
                if (weather.City == city) meteorologicalListByCity.Add(weather);
            }

            return meteorologicalListByCity;
        }

        public MeteorologicalModel? FindByID(Guid id)
        {
            var element = _meteorologicalRepository.ListAll().FirstOrDefault(x => x.Id == id);
            return element;
        }

        public MeteorologicalModel? FindByCityToday(string city)
        {
            DateTime dateToday = DateTime.Now.Date;

            foreach (MeteorologicalModel weather in _meteorologicalRepository.ListAll())
            {
                if (weather.City == city && weather.Date.Date == dateToday)
                {
                    return weather;
                }
            }
            return null;
        }

        public MeteorologicalModel? AddMeteorologicalRegister(MeteorologicalModel weatherModel)
        {
            List<MeteorologicalModel> meteorologicalList = _meteorologicalRepository.ListAll();
            foreach (MeteorologicalModel weather in meteorologicalList)
            {
                if (weather.City == weatherModel.City && weather.Date.Date == weatherModel.Date.Date)
                {
                    return null;
                }
            }
 
            return _meteorologicalRepository.AddMeteorologicalRegister(weatherModel);

        }

        public MeteorologicalModel? DeleteMeteorologicalRegister(Guid id)
        {
            MeteorologicalModel? element = _meteorologicalRepository.ListAll().FirstOrDefault(x => x.Id == id);

            if (element != null) return _meteorologicalRepository.DeleteMeteorologicalRegister(element);
            return null;
        }

        public MeteorologicalModel EditMeteorologicalRegister(Guid id, MeteorologicalModel meteorological)
        {
            Console.WriteLine("#########");
            Console.WriteLine(meteorological.Id);
            meteorological.Id = id;
            Console.WriteLine(meteorological.Id);
            var result = _meteorologicalRepository.EditMeteorologicalRegister(meteorological);
            return result;
        }

        

        public List<MeteorologicalModel> ListNextSeven(string city)
        {

            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel>();

            foreach (MeteorologicalModel weather in _meteorologicalRepository.ListAll())
            {
                DateTime dateTime = DateTime.Now.Date;
                DateTime dateNext7days = dateTime.AddDays(7);

                if (weather.City == city)
                {
                    if (weather.Date.Date > dateTime && weather.Date.Date <= dateNext7days)
                    {
                        meteorologicalListByCity.Add(weather);
                    }
                }
            }

            return meteorologicalListByCity;
        }
    }
}
