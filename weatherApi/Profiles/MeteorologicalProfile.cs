using AutoMapper;
using weatherApi.DTOs;
using weatherApi.Models;

namespace weatherApi.Profiles

{
    public class MeteorologicalProfile : Profile
    {
        public MeteorologicalProfile() 
        {
            CreateMap<MeteorologicalDTO, MeteorologicalModel>();
        }
    }
}
