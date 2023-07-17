using AutoMapper;
using WebApplication_API.Model;
using WebApplication_API.Models.Dto;

namespace WebApplication_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Hotel, HotelDTO>();
            CreateMap<HotelDTO, Hotel>();

            CreateMap<HotelDTO, HotelCreateDTO>().ReverseMap();
            //CreateMap<HotelCreateDTO, HotelDTO>();

            CreateMap<HotelDTO, HotelUpdateDTO>().ReverseMap();
            //CreateMap<HotelUpdateDTO, HotelDTO>();
        }
    }
}
