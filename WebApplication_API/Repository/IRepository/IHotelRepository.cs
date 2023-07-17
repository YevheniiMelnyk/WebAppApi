using System.Linq.Expressions;
using WebApplication_API.Model;

namespace WebApplication_API.Repository.IRepository
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAllHotelAsync(Expression<Func<Hotel, bool>> filter = null);
        Task<Hotel> GetAsync(Expression<Func<Hotel, bool>> filter = null, bool tracked = true);
        Task CreateAsync(Hotel entity);
        Task UpdateAsync(Hotel entity);
        Task RemoveAsync(Hotel entity);
        Task SaveAsync();
    }
}
