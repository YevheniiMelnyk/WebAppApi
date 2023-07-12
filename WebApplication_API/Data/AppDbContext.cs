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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel()
                {
                    Id = 1,
                    Name = "QT Wellington",
                    Description = "Luxury hotel in Te Aro with indoor pool and restaurant",
                    CreatedDate = DateTime.Now,
                    Rate = 100,
                    ImageUrl = "https://images.trvl-media.com/lodging/2000000/1380000/1376400/1376357/b26bd2e2.jpg?impolicy=resizecrop&rw=1200&ra=fit"
                },
                new Hotel()
                {
                    Id = 2,
                    Name = "Naumi Studio Wellington",
                    Description = "Hotel description",
                    CreatedDate = DateTime.Now,
                    Rate = 175,
                    ImageUrl = "https://images.trvl-media.com/lodging/1000000/980000/977400/977353/ba82cfaa.jpg?impolicy=resizecrop&rw=1200&ra=fit"
                },
                new Hotel()
                {
                    Id = 3,
                    Name = "Naumi Auckland Airport",
                    Description = "Suburban hotel with outdoor pool, near Villa Maria Auckland Winery",
                    CreatedDate = DateTime.Now,
                    Rate = 200,
                    ImageUrl = "https://images.trvl-media.com/lodging/11000000/10070000/10062500/10062481/f07817e8.jpg?impolicy=resizecrop&rw=1200&ra=fit"
                },
                new Hotel()
                {
                    Id = 4,
                    Name = "Ramada by Wyndham Wellington Taranaki Street",
                    Description = "Wellington upmarket aparthotel with 24-hour fitness",
                    CreatedDate = DateTime.Now,
                    Rate = 150,
                    ImageUrl = "https://images.trvl-media.com/lodging/67000000/66530000/66522700/66522603/fdaeade6.jpg?impolicy=resizecrop&rw=1200&ra=fit"
                },
                new Hotel()
                {
                    Id = 5,
                    Name = "The Grand by SkyCity",
                    Description = "Luxury hotel with spa, near Sky Tower",
                    CreatedDate = DateTime.Now,
                    Rate = 250,
                    ImageUrl = "https://images.trvl-media.com/lodging/2000000/1390000/1386200/1386172/e654fdfb.jpg?impolicy=resizecrop&rw=1200&ra=fit"
                },
                new Hotel()
                {
                    Id = 6,
                    Name = "M Social Auckland",
                    Description = "Luxury hotel with restaurant, near Princes Warf Visitor Information Centre",
                    CreatedDate = DateTime.Now,
                    Rate = 130,
                    ImageUrl = "https://images.trvl-media.com/lodging/1000000/10000/5500/5465/d4144184.jpg?impolicy=resizecrop&rw=1200&ra=fit"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
