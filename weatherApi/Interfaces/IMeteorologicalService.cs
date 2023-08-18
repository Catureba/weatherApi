using weatherApi.Models;

namespace weatherApi.Interfaces
{
    public interface IMeteorologicalService
    {

        List<MeteorologicalModel> ListAll();
        List<MeteorologicalModel> ListNextSeven(string city);
        MeteorologicalModel FindByID(Guid id);

        List<MeteorologicalModel> FindByCity(string city);

        MeteorologicalModel FindByCityToday(string city);

        MeteorologicalModel AddWeather(MeteorologicalModel weather);

        MeteorologicalModel EditWeather(Guid id, MeteorologicalModel weather);

        MeteorologicalModel DeleteWeather(Guid id);
    }
}
