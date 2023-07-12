using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_API.Data;
using WebApplication_API.Model;
using WebApplication_API.Models.Dto;

namespace WebApplication_API.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public HotelAPIController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            IEnumerable<Hotel> hotelList = await _dbContext.Hotels.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<HotelDTO>>(hotelList));
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(i => i.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HotelDTO>(hotel));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelDTO>> CreateHotel([FromBody] HotelCreateDTO createDTO)
        {
            if (await _dbContext.Hotels.FirstOrDefaultAsync(i => i.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Hotel already exist.");
                return BadRequest(ModelState);
            }

            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            Hotel model = _mapper.Map<Hotel>(createDTO);
            //Hotel model = new()
            //{
            //    Name = createDTO.Name,
            //    Description = createDTO.Description,
            //    Rate = createDTO.Rate,
            //    ImageUrl = createDTO.ImageUrl,
            //    CreatedDate = DateTime.Now
            //};

            await _dbContext.Hotels.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetHotel", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}", Name = "DeleteHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(i => i.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            _dbContext.Hotels.Remove(hotel);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id || id == 0)
            {
                return BadRequest();
            }

            Hotel model = _mapper.Map<Hotel>(updateDTO);
            //Hotel model = new()
            //{
            //    Id = updateDTO.Id,
            //    Name = updateDTO.Name,
            //    Description = updateDTO.Description,
            //    Rate = updateDTO.Rate,
            //    ImageUrl = updateDTO.ImageUrl,
            //    CreatedDate = updateDTO.CreatedDate,
            //    UpdateDate = DateTime.Now
            //};

            _dbContext.Hotels.Update(model);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialHotel(int id, JsonPatchDocument<HotelUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var hotel = await _dbContext.Hotels.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);


            HotelUpdateDTO hotelDTO = _mapper.Map<HotelUpdateDTO>(hotel);
            //HotelUpdateDTO hotelDTO = new()
            //{
            //    Id = hotel.Id,
            //    Name = hotel.Name,
            //    Description = hotel.Description,
            //    Rate = hotel.Rate,
            //    ImageUrl = hotel.ImageUrl,
            //    CreatedDate = hotel.CreatedDate
            //};

            if (hotel == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(hotelDTO, ModelState);

            Hotel model = _mapper.Map<Hotel>(patchDTO);
            //Hotel model = new()
            //{
            //    Id = hotelDTO.Id,
            //    Name = hotelDTO.Name,
            //    Description = hotelDTO.Description,
            //    Rate = hotelDTO.Rate,
            //    ImageUrl = hotelDTO.ImageUrl,
            //    CreatedDate = hotelDTO.CreatedDate,
            //    UpdateDate = DateTime.Now
            //};

            _dbContext.Hotels.Update(model);
            await _dbContext.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
