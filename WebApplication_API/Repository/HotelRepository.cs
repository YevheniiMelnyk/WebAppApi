using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WebApplication_API.Data;
using WebApplication_API.Model;
using WebApplication_API.Repository.IRepository;

namespace WebApplication_API.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _dbContext;

        public HotelRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Hotel entity)
        {
            await _dbContext.Hotels.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Hotel> GetAsync(Expression<Func<Hotel, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Hotel> query = _dbContext.Hotels;
            if (tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Hotel>> GetAllHotelAsync(Expression<Func<Hotel, bool>> filter = null)
        {
            IQueryable<Hotel> query = _dbContext.Hotels;
            if(filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Hotel entity)
        {
            _dbContext.Hotels.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hotel entity)
        {
            _dbContext.Hotels.Update(entity);
            await SaveAsync();
        }
    }
}
