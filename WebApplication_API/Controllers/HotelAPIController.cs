﻿using Microsoft.AspNetCore.JsonPatch;
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

        public HotelAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDTO>> GetHotels()
        {
            return Ok(_dbContext.Hotels.ToList());
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HotelDTO> GetHotel(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var hotel = _dbContext.Hotels.FirstOrDefault(i => i.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HotelDTO> CreateHotel([FromBody] HotelCreateDTO hotelDTO)
        {
            if (_dbContext.Hotels.FirstOrDefault(i => i.Name.ToLower() == hotelDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Hotel already exist.");
                return BadRequest(ModelState);
            }

            if (hotelDTO == null)
            {
                return BadRequest(hotelDTO);
            }

            Hotel model = new()
            {
                Name = hotelDTO.Name,
                Description = hotelDTO.Description,
                Rate = hotelDTO.Rate,
                ImageUrl = hotelDTO.ImageUrl,
                CreatedDate = DateTime.Now
            };

            _dbContext.Hotels.Add(model);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetHotel", new { id = model.Id }, model);
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

            var hotel = _dbContext.Hotels.FirstOrDefault(i => i.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            _dbContext.Hotels.Remove(hotel);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateHotel(int id, [FromBody] HotelUpdateDTO hotelDTO)
        {
            if (hotelDTO == null || id != hotelDTO.Id || id == 0)
            {
                return BadRequest();
            }

            Hotel model = new()
            {
                Id = hotelDTO.Id,
                Name = hotelDTO.Name,
                Description = hotelDTO.Description,
                Rate = hotelDTO.Rate,
                ImageUrl = hotelDTO.ImageUrl,
                CreatedDate = hotelDTO.CreatedDate,
                UpdateDate = DateTime.Now
            };
            _dbContext.Hotels.Update(model);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialHotel(int id, JsonPatchDocument<HotelUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var hotel = _dbContext.Hotels.AsNoTracking().FirstOrDefault(i => i.Id == id);

            HotelUpdateDTO hotelDTO = new()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Description = hotel.Description,
                Rate = hotel.Rate,
                ImageUrl = hotel.ImageUrl,
                CreatedDate = hotel.CreatedDate
            };

            if (hotel == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(hotelDTO, ModelState);

            Hotel model = new()
            {
                Id = hotelDTO.Id,
                Name = hotelDTO.Name,
                Description = hotelDTO.Description,
                Rate = hotelDTO.Rate,
                ImageUrl = hotelDTO.ImageUrl,
                CreatedDate = hotelDTO.CreatedDate,
                UpdateDate = DateTime.Now
            };

            _dbContext.Hotels.Update(model);
            _dbContext.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
