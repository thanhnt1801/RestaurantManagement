using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IPriceRepository
    {
        Task<List<Price>> GetAllAsync();
        Task<Price> GetByIdAsync(int id);
        Task<Price> CreateAsync(Price price);
        Task<Price> UpdateAsync(Price price);
        Task<Price> DeleteAsync(int id);
        Task<Price> RestoreAsync(int id);
    }
}
