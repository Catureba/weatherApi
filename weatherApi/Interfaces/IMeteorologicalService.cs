using weatherApi.Models;

namespace weatherApi.Interfaces
{
    public interface IMeteorologicalService
    {
        List<MeteorologicalModel> ListAll();
        List<MeteorologicalModel> ListNextSeven(string city);
        MeteorologicalModel? FindByID(Guid id);
        List<MeteorologicalModel> FindByCity(string city);
        MeteorologicalModel? FindByCityToday(string city);
        Guid? AddMeteorologicalRegister(MeteorologicalModel meteorological);
        void EditMeteorologicalRegister(Guid id, MeteorologicalModel meteorological);
        void DeleteMeteorologicalRegister(Guid id);
    }
}
