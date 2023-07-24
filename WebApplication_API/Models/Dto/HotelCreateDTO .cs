using System.ComponentModel.DataAnnotations;

namespace WebApplication_API.Models.Dto
{
    public class HotelCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name{ get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
    }
}