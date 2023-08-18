using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace weatherApi.DTOs
{
    public class MeteorologicalDTO
    {
        [Required(ErrorMessage = "City name is required!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Max temperature is required!")]
        public float Max_temperature { get; set; }

        [Required(ErrorMessage = "Min temperature is required!")]
        public float Min_temperature { get; set; }

        [Required(ErrorMessage = "Weather is required!")]
        [EnumDataType(typeof(WeatherType))]
        public WeatherType Weather { get; set; }

        public enum WeatherType
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
