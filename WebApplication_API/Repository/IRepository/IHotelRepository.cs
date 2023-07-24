using WebApplication_API.Model;

namespace WebApplication_API.Repository.IRepository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<Hotel> UpdateAsync(Hotel entity);
    }
}
