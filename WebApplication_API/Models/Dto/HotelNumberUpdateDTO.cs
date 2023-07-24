using System.ComponentModel.DataAnnotations;

namespace WebApplication_API.Models.Dto
{
    public class HotelNumberUpdateDTO
    {
        [Required]
        public int HotelNum { get; set; }
        public string Description { get; set; }
    }
}