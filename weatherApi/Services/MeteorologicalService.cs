using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weatherApi.Data;
using weatherApi.Interfaces;
using weatherApi.Models;

namespace weatherApi.Services
{
    public class MeteorologicalService : IMeteorologicalService
    {
        private MeteorologicalContext _weatherContext;
        public MeteorologicalService(MeteorologicalContext weatherContext) 
        {
            _weatherContext = weatherContext;
        }

        List<MeteorologicalModel> IMeteorologicalService.FindByCity(string city)
        {
            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel>();

            foreach(MeteorologicalModel weather in _weatherContext.Weathers)
            {
                if (weather.City == city) meteorologicalListByCity.Add(weather);
            }
            
            return meteorologicalListByCity;
        }

        MeteorologicalModel? IMeteorologicalService.FindByID(Guid id)
        {
            var element = _weatherContext.Weathers.FirstOrDefault(x => x.Id == id);
            return element;
        }

        MeteorologicalModel? IMeteorologicalService.FindByCityToday(string city)
        {
            DateTime dateToday = DateTime.Now.Date;

            foreach (MeteorologicalModel weather in _weatherContext.Weathers)
            {
                if (weather.City == city && weather.Date.Date == dateToday)
                {
                    return weather;
                }
            }
            return null;
        }

        MeteorologicalModel? IMeteorologicalService.AddWeather(MeteorologicalModel weatherModel)
        {
            foreach (MeteorologicalModel weather in _weatherContext.Weathers)
            {
                if (weather.City == weatherModel.City && weather.Date.Date == weatherModel.Date.Date)
                {
                    return null;
                }
            }

            _weatherContext.Weathers.Add(weatherModel);
            _weatherContext.SaveChanges();
            return weatherModel;

        }

        MeteorologicalModel? IMeteorologicalService.DeleteWeather(Guid id)
        {
            var element = _weatherContext.Weathers.FirstOrDefault(x => x.Id == id);

            if (element != null)
            {
                _weatherContext.Weathers.Remove(element);
                _weatherContext.SaveChanges();
                return element;
            }
            return null;
        }

        MeteorologicalModel? IMeteorologicalService.EditWeather(Guid id, MeteorologicalModel weather)
        {
            var old = _weatherContext.Weathers.FirstOrDefault(x => x.Id == id);

            if (old != null)
            {
                old.City = weather.City;
                old.Date = weather.Date;
                old.Max_temperature = weather.Max_temperature;
                old.Min_temperature = weather.Min_temperature;
                old.Weather = weather.Weather;
                _weatherContext.SaveChanges();
                return weather;
            }
            return null;
        }

        List<MeteorologicalModel> IMeteorologicalService.ListAll()
        {
            var meteorologicalList = _weatherContext.Weathers.ToList();
            return meteorologicalList;
        }

        List<MeteorologicalModel> IMeteorologicalService.ListNextSeven(string city)
        {

            List<MeteorologicalModel> meteorologicalListByCity = new List<MeteorologicalModel>();

            foreach (MeteorologicalModel weather in _weatherContext.Weathers)
            {
                DateTime dateTime = DateTime.Now;
                DateTime dateNext7days = dateTime.AddDays(7);
                if (weather.City == city)
                {
                    if (weather.Date > dateTime && weather.Date < dateNext7days)
                    {
                        meteorologicalListByCity.Add(weather);
                    }
                }
            }

            return meteorologicalListByCity;
        }
    }
}
