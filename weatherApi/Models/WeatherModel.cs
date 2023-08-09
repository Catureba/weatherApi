using System.ComponentModel.DataAnnotations;

namespace weatherApi.Models
{
    public class WeatherModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public DateOnly Data { get; set; }

        [Required]
        public float Max_temperature { get; set; }

        [Required]
        public float Min_temperature { get; set; }

        [Required]
        public WeatherType Weater { get; set; }
        public WeatherModel() { }

        public enum WeatherType
        {
            RAINY,
            SUNNY,
            OVERCAST,
        }

    }
}
