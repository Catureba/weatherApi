using weatherApi.Models;

namespace weatherApi.Data.Repository
{
    public interface IMeteorologicalRepository
    {
        Task<List<MeteorologicalModel>> ListAll();
        Task<List<MeteorologicalModel>> ListNextSevenAsync(string city);
        Task<MeteorologicalModel?> FindByID(Guid id);
        Task<List<MeteorologicalModel>> FindByCity(string city);
        Task<MeteorologicalModel?> FindByCityToday(string city);
        Task<MeteorologicalModel?> AddMeteorologicalRegister(MeteorologicalModel meteorological);
        Task<MeteorologicalModel?> EditMeteorologicalRegister(MeteorologicalModel meteorological);
        Task DeleteMeteorologicalRegister(MeteorologicalModel meteorological);
    }
}
