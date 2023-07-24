using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication_API.Model;
using WebApplication_API.Models;
using WebApplication_API.Models.Dto;
using WebApplication_API.Repository.IRepository;

namespace WebApplication_API.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IHotelRepository _dbHotel;
        private readonly IMapper _mapper;

        public HotelAPIController(IHotelRepository dbHotel, IMapper mapper)
        {
            _dbHotel = dbHotel;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetHotels()
        {
            try
            {
                IEnumerable<Hotel> hotelList = await _dbHotel.GetAllHotelAsync();
                _response.Result = _mapper.Map<IEnumerable<HotelDTO>>(hotelList);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHotel(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var hotel = await _dbHotel.GetAsync(i => i.Id == id);
                if (hotel == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<HotelDTO>(hotel);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.ToString() };
            }
            return _response;
            //return Ok(_mapper.Map<HotelDTO>(hotel));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateHotel([FromBody] HotelCreateDTO createDTO)
        {
            try
            {
                if (await _dbHotel.GetAsync(i => i.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Hotel already exist.");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //Hotel model = _mapper.Map<Hotel>(createDTO);
                //model.CreatedDate = DateTime.Now;

                Hotel hotel = new()
                {
                    Description = createDTO.Description,
                    ImageUrl = createDTO.ImageUrl,
                    Name = createDTO.Name,
                    Rate = createDTO.Rate
                };
                hotel.CreatedDate = DateTime.Now;

                await _dbHotel.CreateAsync(hotel);

                _response.Result = _mapper.Map<HotelDTO>(hotel);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetHotel", new { id = hotel.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteHotel(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var hotel = await _dbHotel.GetAsync(i => i.Id == id);
                if (hotel == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbHotel.RemoveAsync(hotel);
                _response.Result = _mapper.Map<HotelDTO>(hotel);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateHotel(int id, [FromBody] HotelUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id || id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //Hotel model = _mapper.Map<Hotel>(updateDTO); 
                Hotel model = new()
                {
                    Id = updateDTO.Id,
                    Description = updateDTO.Description,
                    ImageUrl = updateDTO.ImageUrl,
                    Name = updateDTO.Name,
                    Rate = updateDTO.Rate
                };

                await _dbHotel.UpdateAsync(model);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePartialHotel(int id, JsonPatchDocument<HotelUpdateDTO> patchDTO)
        {
            try
            {
                if (patchDTO == null || id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var hotel = await _dbHotel.GetAsync(i => i.Id == id, tracked: false);

                //HotelUpdateDTO hotelDTO = _mapper.Map<HotelUpdateDTO>(hotel);
                HotelUpdateDTO hotelDTO = new()
                {
                    Id = hotel.Id,
                    Description = hotel.Description,
                    ImageUrl = hotel.ImageUrl,
                    Name = hotel.Name,
                    Rate = hotel.Rate
                };

                if (hotel == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                patchDTO.ApplyTo(hotelDTO, ModelState);

                //Hotel model = _mapper.Map<Hotel>(patchDTO);
                Hotel model = new()
                {
                    Id = hotelDTO.Id,
                    Description = hotelDTO.Description,
                    ImageUrl = hotelDTO.ImageUrl,
                    Name = hotelDTO.Name,
                    Rate = hotelDTO.Rate
                };
                model.UpdateDate = DateTime.Now;

                await _dbHotel.UpdateAsync(model);

                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Errors = new List<string> { "ModelState is invalid." };
                    return BadRequest(_response);
                }

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
