using System.ComponentModel.DataAnnotations;

namespace WebApplication_API.Models.Dto
{
    public class HotelUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name{ get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}