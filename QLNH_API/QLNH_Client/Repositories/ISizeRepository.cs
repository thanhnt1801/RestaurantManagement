using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface ISizeRepository
    {
        Task<List<Size>> GetAllAsync();
        Task<Size> GetByIdAsync(int id);
        Task<Size> CreateAsync(Size size);
        Task<Size> UpdateAsync(Size size);
        Task<Size> DeleteAsync(int id);
        Task<Size> RestoreAsync(int id);
    }
}
