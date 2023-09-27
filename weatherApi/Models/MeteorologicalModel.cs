using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace weatherApi.Models
{
    public class MeteorologicalModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "City name is required!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Max temperature is required!")]
        public float Max_temperature { get; set; }

        [Required(ErrorMessage = "Min temperature is required!")]
        public float Min_temperature { get; set; }

        [Required(ErrorMessage ="Weather day is required!")]
        public string Weather_day { get; set; }

        [Required(ErrorMessage = "Weather night is required!")]
        public string Weather_night { get; set; }

        [Required(ErrorMessage = "Precipitation is required!")]
        public float Precipitation { get; set; }

        [Required(ErrorMessage = "Humidity is required!")]
        public string Humidity { get; set; }

        [Required(ErrorMessage = "Wind speed is required!")]
        public string Wind_speed { get; set; }

        //[Required(ErrorMessage = "Weather is required!")]
        //[EnumDataType(typeof(WeatherType))]
        //public WeatherType Weather { get; set; }

        //public enum WeatherType
        //{
        //    [EnumMember(Value = "RAINY")]
        //    RAINY = 0,
        //    [EnumMember(Value = "SUNNY")]
        //    SUNNY = 1,
        //    [EnumMember(Value = "OVERCAST")]
        //    OVERCAST = 2,
        //    [EnumMember(Value = "TROPICAIS")]
        //    TROPICAIS = 3,
        //    [EnumMember(Value = "SHAKESPEARE")]
        //    SHAKESPEARE = 4,
        //}

    }
}
