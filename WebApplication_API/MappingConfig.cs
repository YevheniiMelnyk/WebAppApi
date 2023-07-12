using AutoMapper;
using WebApplication_API.Model;
using WebApplication_API.Models.Dto;

namespace WebApplication_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<HotelDTO, HotelCreateDTO>().ReverseMap();
            CreateMap<HotelDTO, HotelUpdateDTO>().ReverseMap();
        }
    }
}
