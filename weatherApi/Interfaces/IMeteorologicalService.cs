using weatherApi.Models;

namespace weatherApi.Interfaces
{
    public interface IMeteorologicalService
    {
        Task<MeteorologicalList> ListWithPagination(int skip, string city);
        Task<List<MeteorologicalModel>> ListAll();
        Task<List<MeteorologicalModel>> ListNextSeven(string city);
        Task<MeteorologicalModel?> FindByID(Guid id);
        Task<List<MeteorologicalModel>> FindByCity(string city);
        Task<MeteorologicalModel?> FindByCityToday(string city);
        Task<MeteorologicalModel?> AddMeteorologicalRegister(MeteorologicalModel meteorological);
        Task<MeteorologicalModel?> EditMeteorologicalRegister(Guid id, MeteorologicalModel meteorological);
        Task DeleteMeteorologicalRegister(Guid id);
        Task<Boolean> ValidateExistMeteorologicalRegister(MeteorologicalModel meteorological);
    }
}
