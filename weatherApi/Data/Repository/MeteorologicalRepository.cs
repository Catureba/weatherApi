using AutoMapper;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using weatherApi.Models;

namespace weatherApi.Data.Repository
{
    public class MeteorologicalRepository : IMeteorologicalRepository
    {
        private MeteorologicalContext _context;
        private IMapper _mapper;
        public MeteorologicalRepository(MeteorologicalContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        
        
        public List<MeteorologicalModel> ListAll()
        {
            return _context.Weathers.ToList();
        }
        public List<MeteorologicalModel> ListNextSeven(string city)
        {
            DateTime dateToday = DateTime.Now.Date;
            DateTime todayMoreSevenDays = dateToday.AddDays(7);
            var listByCity = new List<MeteorologicalModel>();
            foreach (MeteorologicalModel meteorological in FindByCity(city))
            {
                if (meteorological.Date.Date > dateToday && meteorological.Date.Date <= todayMoreSevenDays)
                {
                    listByCity.Add(meteorological);
                }
            }
            return listByCity;
        }
        public MeteorologicalModel? FindByID(Guid id)
        {
            return _context.Weathers.FirstOrDefault(x => x.Id == id);
        }
        public List<MeteorologicalModel> FindByCity(string city)
        {
            var response = _context.Weathers.Where(x => x.City == city);
            return response.ToList();
        }
        public MeteorologicalModel? FindByCityToday(string city)
        {
            DateTime dateToday = DateTime.Now.Date;
            var meteorologicalModelsByCity = _context.Weathers.Where(x => x.City == city);
            foreach (MeteorologicalModel weather in meteorologicalModelsByCity) if (weather.Date.Date == dateToday) return weather;
            return null;
        }
        public MeteorologicalModel? AddMeteorologicalRegister(MeteorologicalModel meteorologicalModel)
        {
            var responseEntity = _context.Weathers.Add(meteorologicalModel);
            MeteorologicalModel response = _mapper.Map<MeteorologicalModel>(responseEntity.Entity);
            _context.SaveChanges();

            return response;
        }
        public MeteorologicalModel? EditMeteorologicalRegister(MeteorologicalModel meteorological)
        {
            var byEdit = _context.Weathers.FirstOrDefault(x => x.Id == meteorological.Id);
            if(byEdit != null)
            {
                var saved = _context.Weathers.Update(byEdit);
                _context.SaveChanges();
                return _mapper.Map<MeteorologicalModel>(saved.Entity);
            }
            return null;
        }

        public void DeleteMeteorologicalRegister(MeteorologicalModel meteorological)
        {
            _context.Weathers.Remove(meteorological);
            _context.SaveChanges();
        }

    }
}
