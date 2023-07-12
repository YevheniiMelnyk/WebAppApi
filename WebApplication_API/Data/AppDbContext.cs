using Microsoft.EntityFrameworkCore;
using WebApplication_API.Model;

namespace WebApplication_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
