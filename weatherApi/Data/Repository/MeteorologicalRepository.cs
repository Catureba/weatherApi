using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        
        
        public async Task<List<MeteorologicalModel>> ListAll()
        {
            List<MeteorologicalModel> listAll = new List<MeteorologicalModel>();
            await Task.Run(() =>
            {
                listAll = _context.Weathers.ToList();
                
            });

            return listAll;
        }

        public async Task<List<MeteorologicalModel>> ListNextSevenAsync(string city)
        {
            DateTime dateToday = DateTime.UtcNow.Date;
            DateTime todayMoreSevenDays = dateToday.AddDays(7);

            

            var listByCity = await _context.Weathers
                .Where(x => 
                    x.City == city.ToLower() &&
                    x.Date.Date > dateToday && 
                    x.Date.Date <= todayMoreSevenDays).ToListAsync();

            return listByCity;
        }

        public async Task<MeteorologicalModel?> FindByID(Guid id)
        {
            return _context.Weathers.FirstOrDefault(x => x.Id == id);
        }
        public async Task<List<MeteorologicalModel>> FindByCity(string city)
        {
            return await _context.Weathers.Where(x => x.City == city.ToLower()).ToListAsync(); ;
        }

        public async Task<MeteorologicalModel?> FindByCityToday(string city)
        {
            DateTime dateToday = DateTime.UtcNow.Date;
            return _context.Weathers.Where(x => x.City == city.ToLower()).FirstOrDefault(element => element.Date.Date == dateToday);
        }
        public async Task<MeteorologicalModel?> AddMeteorologicalRegister(MeteorologicalModel meteorologicalModel)
        {
            var responseEntity = _context.Weathers.Add(meteorologicalModel);
            MeteorologicalModel response = _mapper.Map<MeteorologicalModel>(responseEntity.Entity);
            _context.SaveChanges();

            return response;
        }
        public async Task<MeteorologicalModel?> EditMeteorologicalRegister(MeteorologicalModel meteorological)
        {
            MeteorologicalModel? byEdit = _context.Weathers.FirstOrDefault(x => x.Id == meteorological.Id);
            if (byEdit != null)
            {
                byEdit.Id = meteorological.Id;
                byEdit.City = meteorological.City;
                byEdit.Date = meteorological.Date;
                byEdit.Humidity = meteorological.Humidity;
                byEdit.Min_temperature = meteorological.Min_temperature;
                byEdit.Max_temperature = meteorological.Max_temperature;
                byEdit.Wind_speed = meteorological.Wind_speed;
                byEdit.Weather_day = meteorological.Weather_day;
                byEdit.Weather_night = meteorological.Weather_night;
                byEdit.Precipitation = meteorological.Precipitation;

                _context.Entry(byEdit).State = EntityState.Modified;
                _context.SaveChanges();

                return _mapper.Map<MeteorologicalModel>(byEdit);
            }
            return null;
        }


        public async Task DeleteMeteorologicalRegister(MeteorologicalModel meteorological)
        {
            _context.Weathers.Remove(meteorological);
            _context.SaveChanges();
        }

    }
}
