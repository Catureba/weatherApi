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
        Guid AddMeteorologicalRegister(MeteorologicalModel meteorological);
        void EditMeteorologicalRegister(MeteorologicalModel meteorological);
        void DeleteMeteorologicalRegister(MeteorologicalModel meteorological);
    }
}
