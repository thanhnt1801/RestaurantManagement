using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IGuestRepository
    {
        Task<List<Guest>> GetAllAsync();
        Task<Guest> GetByIdAsync(int id);
        Task<Guest> CreateAsync(Guest guest);
        Task<Guest> UpdateAsync(Guest guest);
        Task<Guest> DeleteAsync(int id);
        Task<Guest> RestoreAsync(int id);
    }
}
