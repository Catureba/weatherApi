namespace weatherApi.Models
{
    public class MeteorologicalList
    {
        public int totalRegisters { get; set; }
        public List<MeteorologicalModel> data { get; set; }
    }
}
