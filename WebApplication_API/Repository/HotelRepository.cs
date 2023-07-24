using WebApplication_API.Data;
using WebApplication_API.Model;
using WebApplication_API.Repository.IRepository;

namespace WebApplication_API.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly AppDbContext _dbContext;

        public HotelRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Hotel> UpdateAsync(Hotel entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbContext.Hotels.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
