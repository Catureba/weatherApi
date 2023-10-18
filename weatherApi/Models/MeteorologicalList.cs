namespace weatherApi.Models
{
    public class MeteorologicalList
    {
        public int totalRegisters { get; set; }
        public int totalPages { get; set; }
        public int atualPage { get; set; }
        public List<MeteorologicalModel> data { get; set; }
    }
}
