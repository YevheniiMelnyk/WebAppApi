using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.Model
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedDate{ get; set; }
        public DateTime? UpdateDate{ get; set; }
    }
}
