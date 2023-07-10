using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllAsync();
        Task<Location> GetByIdAsync(int id);
        Task<Location> CreateAsync(Location location);
        Task<Location> UpdateAsync(Location location);
        Task<Location> DeleteAsync(int id);
        Task<Location> RestoreAsync(int id);
    }
}
