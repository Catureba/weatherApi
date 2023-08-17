using weatherApi.Models;

namespace weatherApi.Interfaces
{
    public interface IWeatherService
    {

        List<WeatherModel> ListAll();
        List<WeatherModel> ListNextSeven(string city);
        WeatherModel FindByID(Guid id);

        List<WeatherModel> FindByCity(string city);

        WeatherModel AddWeather(WeatherModel weather);

        WeatherModel EditWeather(Guid id, WeatherModel weather);

        WeatherModel DeleteWeather(Guid id);
    }
}
