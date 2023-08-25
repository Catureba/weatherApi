using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
        public List<MeteorologicalModel> ListNextSeven(string city)
        {
            List<MeteorologicalModel> meteorologicalListByCity = _meteorologicalRepository.ListNextSeven(city);
            return meteorologicalListByCity;
        }
        public List<MeteorologicalModel> FindByCity(string city)
        {
            var meteorologicalListByCity = _meteorologicalRepository.FindByCity(city);
            return meteorologicalListByCity;
        }
        public MeteorologicalModel? FindByCityToday(string city)
        {
            return _meteorologicalRepository.FindByCityToday(city);
        }
        public MeteorologicalModel? FindByID(Guid id)
        {
             return _meteorologicalRepository.ListAll().FirstOrDefault(x => x.Id == id);
        }
        public Guid? AddMeteorologicalRegister(MeteorologicalModel meteorologicalModel)
        {
            meteorologicalModel.City = meteorologicalModel.City.ToLower();
            List<MeteorologicalModel> meteorologicalListByCity = _meteorologicalRepository.FindByCity(meteorologicalModel.City);
            meteorologicalListByCity = meteorologicalListByCity.Where(x => x.Date.Date == meteorologicalModel.Date.Date).ToList();
            if (meteorologicalListByCity.Any()) throw new Exception("Request declined: You can only have one record per day for each city.");
            return _meteorologicalRepository.AddMeteorologicalRegister(meteorologicalModel);
        }
        public void EditMeteorologicalRegister(Guid id, MeteorologicalModel meteorological)
        {
            MeteorologicalModel? idExist = _meteorologicalRepository.FindByID(id);
            if (idExist == null) throw new Exception("Registro não encontrado");
            meteorological.Id = id;
            _meteorologicalRepository.EditMeteorologicalRegister(meteorological);
        }
        public void DeleteMeteorologicalRegister(Guid id)
        {
            MeteorologicalModel? meteorological = _meteorologicalRepository.FindByID(id);
            if (meteorological == null) throw new Exception("Registro não encontrado");
            _meteorologicalRepository.DeleteMeteorologicalRegister(meteorological);
        }
    }
}
