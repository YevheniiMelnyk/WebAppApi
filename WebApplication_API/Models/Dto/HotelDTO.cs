using System.ComponentModel.DataAnnotations;

namespace WebApplication_API.Models.Dto
{
    public class HotelDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name{ get; set; }
    }
}
