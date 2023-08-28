using weatherApi.Models;

namespace weatherApi.Data.Repository
{
    public interface IMeteorologicalRepository
    {
        List<MeteorologicalModel> ListAll();
        List<MeteorologicalModel> ListNextSeven(string city);
        MeteorologicalModel? FindByID(Guid id);
        List<MeteorologicalModel> FindByCity(string city);
        MeteorologicalModel? FindByCityToday(string city);
        MeteorologicalModel? AddMeteorologicalRegister(MeteorologicalModel meteorological);
        MeteorologicalModel? EditMeteorologicalRegister(MeteorologicalModel meteorological);
        void DeleteMeteorologicalRegister(MeteorologicalModel meteorological);
    }
}
