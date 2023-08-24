using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using weatherApi.Models;
using static weatherApi.Models.MeteorologicalModel;

namespace weatherApi.Data
{
    public class MeteorologicalContext : DbContext
    {
        public MeteorologicalContext( DbContextOptions<MeteorologicalContext> opts) : base(opts) 
        {
            
        }

        public DbSet<MeteorologicalModel> Weathers { get; set; }

    }
}
