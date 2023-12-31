﻿using AutoMapper;
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

        public MeteorologicalList ListWithPagination(int skip, string city = "")
        {
            int pageSize = 5;
            if(city == "")
            {
                var meteorologicalList = _meteorologicalRepository.ListAll();

                MeteorologicalList allRegisters = new MeteorologicalList
                {
                    data = meteorologicalList.Skip(skip * pageSize).Take(5).ToList(),
                    totalRegisters = meteorologicalList.Count,
                    totalPages = (meteorologicalList.Count + 5 - 1) / 5,
                    currentPage = skip,
                };
                return allRegisters;
            }
            else
            {
                var meteorologicalList = _meteorologicalRepository.FindByCity(city);

                MeteorologicalList allRegisters = new MeteorologicalList
                {
                    data = meteorologicalList.Skip(skip * pageSize).Take(5).ToList(),
                    totalRegisters = meteorologicalList.Count,
                    totalPages = (meteorologicalList.Count + 5 - 1) / 5,
                    currentPage = skip,
                };
                return allRegisters;
            }
            
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
        public MeteorologicalModel? AddMeteorologicalRegister(MeteorologicalModel meteorologicalModel)
        {
            meteorologicalModel.City = meteorologicalModel.City.ToLower();
            if(!ValidateExistMeteorologicalRegister(meteorologicalModel)) return null;
            return _meteorologicalRepository.AddMeteorologicalRegister(meteorologicalModel);
        }
        public MeteorologicalModel? EditMeteorologicalRegister(Guid id, MeteorologicalModel meteorological)
        {
            MeteorologicalModel? idExist = _meteorologicalRepository.FindByID(id);
            if (idExist == null) return null; //throw new Exception("Registro não encontrado");
            meteorological.Id = id;
            meteorological.City = meteorological.City.ToLower();
            var result = _meteorologicalRepository.EditMeteorologicalRegister(meteorological);

            return result;
        }
        public void DeleteMeteorologicalRegister(Guid id)
        {
            MeteorologicalModel? meteorological = _meteorologicalRepository.FindByID(id);
            if (meteorological == null) throw new Exception("Registro não encontrado");
            _meteorologicalRepository.DeleteMeteorologicalRegister(meteorological);
        }

        public Boolean ValidateExistMeteorologicalRegister(MeteorologicalModel meteorological) 
        {
            List<MeteorologicalModel> meteorologicalListByCity = _meteorologicalRepository.FindByCity(meteorological.City);
            if (meteorologicalListByCity != null) return !meteorologicalListByCity.Any(x => x.Date.Date == meteorological.Date.Date);
            return true;
        }
    }
}
