using weatherApi.Models;

namespace weatherApi.Interfaces
{
    public interface IMeteorologicalService
    {
        MeteorologicalList ListAll();
        List<MeteorologicalModel> ListNextSeven(string city);
        MeteorologicalModel? FindByID(Guid id);
        MeteorologicalList FindByCity(string city);
        MeteorologicalModel? FindByCityToday(string city);
        MeteorologicalModel? AddMeteorologicalRegister(MeteorologicalModel meteorological);
        MeteorologicalModel? EditMeteorologicalRegister(Guid id, MeteorologicalModel meteorological);
        void DeleteMeteorologicalRegister(Guid id);
        Boolean ValidateExistMeteorologicalRegister(MeteorologicalModel meteorological);
    }
}
