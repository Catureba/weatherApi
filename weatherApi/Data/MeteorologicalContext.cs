using Microsoft.EntityFrameworkCore;
using weatherApi.Models;

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
