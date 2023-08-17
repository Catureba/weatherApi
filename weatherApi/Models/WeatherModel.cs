using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace weatherApi.Models
{
    public class WeatherModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public float Max_temperature { get; set; }

        [Required]
        public float Min_temperature { get; set; }

        [Required]
        [EnumDataType(typeof(WeatherType))]
        public WeatherType Weather { get; set; }
        public WeatherModel() { }

        public enum WeatherType : int
        {
            [EnumMember(Value = "RAINY")]
            RAINY = 0,
            [EnumMember(Value = "SUNNY")]
            SUNNY = 1,
            [EnumMember(Value = "OVERCAST")]
            OVERCAST = 2,
            [EnumMember(Value = "TROPICAIS")]
            TROPICAIS = 3,
            [EnumMember(Value = "SHAKESPEARE")]
            SHAKESPEARE = 4,
        }

    }
}
