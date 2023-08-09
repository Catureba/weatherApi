using Microsoft.EntityFrameworkCore;
using weatherApi.Models;

namespace weatherApi.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext( DbContextOptions<WeatherContext> opts) : base(opts) 
        {
            
        }

        public DbSet<WeatherModel> Weathers { get; set; }

    }
}
