using WebApplication_API.Models.Dto;

namespace WebApplication_API.Data
{
    public static class HotelStore
    {
        public static List<HotelDTO> hotelList = new()
        {
            new HotelDTO { Id = 1, Name = "Menalo Hotel Premium", Price = 200 }, 
            new HotelDTO { Id = 2, Name = "The Green Park Ankara", Price = 400 }
        };
    }
}
