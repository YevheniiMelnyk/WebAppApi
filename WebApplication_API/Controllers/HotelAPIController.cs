using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_API.Data;
using WebApplication_API.Model;
using WebApplication_API.Models.Dto;

namespace WebApplication_API.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        [HttpGet("GetAllHotels")]
        public IEnumerable<HotelDTO> GetHotels()
        {
            return HotelStore.hotelList;
        }

        [HttpGet("GetHotelById")]
        public HotelDTO GetHotel(int hotelId)
        {
            return HotelStore.hotelList.FirstOrDefault(i => i.Id == hotelId);
        }
    }
}
