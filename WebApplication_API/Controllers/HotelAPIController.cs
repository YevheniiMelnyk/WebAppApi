using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_API.Model;

namespace WebApplication_API.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Hotel> GetHotels()
        {
            return new List<Hotel> 
            {
                new Hotel { Id = 1, Name = "Menalo Hotel Premium"}, 
                new Hotel { Id = 2, Name = "The Green Park Ankara"}
            };
        }
    }
}
