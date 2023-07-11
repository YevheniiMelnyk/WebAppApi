using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApplication_API.Data;
using WebApplication_API.Models.Dto;

namespace WebApplication_API.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDTO>> GetHotels()
        {
            return Ok(HotelStore.hotelList);
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HotelDTO> GetHotel(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var hotel = HotelStore.hotelList.FirstOrDefault(i => i.Id == id);
            if(hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HotelDTO> CreateHotel([FromBody]HotelDTO hotelDTO)
        {
            if(HotelStore.hotelList.FirstOrDefault(i => i.Name.ToLower() == hotelDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Hotel already exist.");
                return BadRequest(ModelState);
            }

            if(hotelDTO == null)
            {
                return BadRequest(hotelDTO);
            }

            if(hotelDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            hotelDTO.Id = HotelStore.hotelList.OrderByDescending(i => i.Id).FirstOrDefault().Id + 1;
            HotelStore.hotelList.Add(hotelDTO);

            return CreatedAtRoute("GetHotel", new { id = hotelDTO.Id }, hotelDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHotel(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var hotel = HotelStore.hotelList.FirstOrDefault(i => i.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            HotelStore.hotelList.Remove(hotel);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateHotel(int id, [FromBody] HotelDTO hotelDTO)
        {
            if(hotelDTO == null || id != hotelDTO.Id || id == 0)
            {
                return BadRequest();
            }

            var hotel = HotelStore.hotelList.FirstOrDefault(i => i.Id == id);
            hotel.Name = hotelDTO.Name;
            hotel.Price = hotelDTO.Price;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialHotel(int id, JsonPatchDocument<HotelDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var hotel = HotelStore.hotelList.FirstOrDefault(i => i.Id == id);
            if(hotel == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(hotel, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
