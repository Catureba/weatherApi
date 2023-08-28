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
        MeteorologicalModel? AddMeteorologicalRegister(MeteorologicalModel meteorological);
        MeteorologicalModel? EditMeteorologicalRegister(Guid id, MeteorologicalModel meteorological);
        void DeleteMeteorologicalRegister(Guid id);
        Boolean ValidateExistMeteorologicalRegister(MeteorologicalModel meteorological);
    }
}
