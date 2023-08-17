using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weatherApi.Data;
using weatherApi.Interfaces;
using weatherApi.Models;

namespace weatherApi.Services
{
    public class WeatherService : IWeatherService
    {
        private WeatherContext _weatherContext;
        public WeatherService(WeatherContext weatherContext) 
        {
            _weatherContext = weatherContext;
        }

        List<WeatherModel>? IWeatherService.FindByCity(string city)
        {
            List<WeatherModel> model = new List<WeatherModel>();

            foreach(WeatherModel weather in _weatherContext.Weathers)
            {
                if (weather.City == city) model.Add(weather);
            }
            
            return model == null ? null : model;
        }

        WeatherModel? IWeatherService.FindByID(Guid id)
        {
            var element = _weatherContext.Weathers.FirstOrDefault(x => x.Id == id);
            return element == null ? null : element;
        }

        WeatherModel? IWeatherService.AddWeather(WeatherModel weather)
        {
            if (weather != null)
            {
                _weatherContext.Weathers.Add(weather);
                _weatherContext.SaveChanges();
                return weather;
            }
            return null;
        }

        WeatherModel? IWeatherService.DeleteWeather(Guid id)
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

        WeatherModel? IWeatherService.EditWeather(Guid id, WeatherModel weather)
        {
            if (weather != null)
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
                }
                return weather;
            }
            return null;
        }

        List<WeatherModel> IWeatherService.ListAll()
        {
            return _weatherContext.Weathers.ToList();
        }

        List<WeatherModel>? IWeatherService.ListNextSeven(string city)
        {

            List<WeatherModel> model = new List<WeatherModel>();

            foreach (WeatherModel weather in _weatherContext.Weathers)
            {
                DateTime dateTime = DateTime.Now;
                DateTime dateNext7days = dateTime.AddDays(7);
                if (weather.City == city)
                {
                    if (weather.Date > dateTime && weather.Date < dateNext7days)
                    {
                        model.Add(weather);
                    }
                }
            }

            return model == null ? null : model;
        }
    }
}
