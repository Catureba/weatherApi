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

        public async Task<MeteorologicalList> ListWithPagination(int skip, string city = "")
        {
            int pageSize = 5;
            if(city == "")
            {
                var meteorologicalList = await _meteorologicalRepository.ListAll();

                MeteorologicalList allRegisters = new MeteorologicalList
                {
                    data = meteorologicalList.Skip(skip * pageSize).Take(5).ToList(),
                    totalRegisters = meteorologicalList.Count,
                    totalPages = (meteorologicalList.Count + 5 - 1) / 5,
                    currentPage = skip,
                };
                return allRegisters;
            }

            var meteorologicalListByCity = await _meteorologicalRepository.FindByCity(city);
            MeteorologicalList allRegistersByCity = new MeteorologicalList
            {
                data = meteorologicalListByCity.Skip(skip * pageSize).Take(5).ToList(),
                totalRegisters = meteorologicalListByCity.Count,
                totalPages = (meteorologicalListByCity.Count + 5 - 1) / 5,
                currentPage = skip,
            };
            return allRegistersByCity;
            
        }
        public async Task<List<MeteorologicalModel>> ListAll()
        {
            var meteorologicalList = await _meteorologicalRepository.ListAll();
            return meteorologicalList;
        }
        public async Task<List<MeteorologicalModel>> ListNextSeven(string city)
        {
            var meteorologicalListByCity = await _meteorologicalRepository.ListNextSevenAsync(city);
            return meteorologicalListByCity;
        }
        public async Task<List<MeteorologicalModel>> FindByCity(string city)
        {
            var meteorologicalListByCity = await _meteorologicalRepository.FindByCity(city);
            return meteorologicalListByCity;
        }
        public async Task<MeteorologicalModel?> FindByCityToday(string city)
        {
            return await _meteorologicalRepository.FindByCityToday(city);
        }
        public async Task<MeteorologicalModel?> FindByID(Guid id)
        {
             return await _meteorologicalRepository.FindByID(id);
        }
        public async Task<MeteorologicalModel?> AddMeteorologicalRegister(MeteorologicalModel meteorologicalModel)
        {
            meteorologicalModel.City = meteorologicalModel.City.ToLower();
            if(!await ValidateExistMeteorologicalRegister(meteorologicalModel)) return null;
            return await _meteorologicalRepository.AddMeteorologicalRegister(meteorologicalModel);
        }
        public async Task<MeteorologicalModel?> EditMeteorologicalRegister(Guid id, MeteorologicalModel meteorological)
        {
            MeteorologicalModel? idExist = await _meteorologicalRepository.FindByID(id);
            if (idExist == null) return null;
            meteorological.Id = id;
            meteorological.City = meteorological.City.ToLower();
            var result = await _meteorologicalRepository.EditMeteorologicalRegister(meteorological);

            return result;
        }
        public async Task DeleteMeteorologicalRegister(Guid id)
        {
            MeteorologicalModel? meteorological = await _meteorologicalRepository.FindByID(id);
            if (meteorological == null) throw new Exception("Registro não encontrado");
            await _meteorologicalRepository.DeleteMeteorologicalRegister(meteorological);
        }

        public async Task<Boolean> ValidateExistMeteorologicalRegister(MeteorologicalModel meteorological) 
        {
            List<MeteorologicalModel> meteorologicalListByCity = await _meteorologicalRepository.FindByCity(meteorological.City);
            if (meteorologicalListByCity != null) return !meteorologicalListByCity.Any(x => x.Date.Date == meteorological.Date.Date);
            return true;
        }
    }
}
