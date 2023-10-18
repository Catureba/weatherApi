using weatherApi.Models;

namespace weatherApi.Data.Repository
{
    public interface IMeteorologicalRepository
    {
        List<MeteorologicalModel> ListAll();
        List<MeteorologicalModel> ListWithPagination(int skip);
        List<MeteorologicalModel> ListNextSeven(string city);
        MeteorologicalModel? FindByID(Guid id);
        List<MeteorologicalModel> FindByCity(string city);
        List<MeteorologicalModel> FindByCityWithPagination(string city, int skip);
        MeteorologicalModel? FindByCityToday(string city);
        MeteorologicalModel? AddMeteorologicalRegister(MeteorologicalModel meteorological);
        MeteorologicalModel? EditMeteorologicalRegister(MeteorologicalModel meteorological);
        void DeleteMeteorologicalRegister(MeteorologicalModel meteorological);
    }
}
