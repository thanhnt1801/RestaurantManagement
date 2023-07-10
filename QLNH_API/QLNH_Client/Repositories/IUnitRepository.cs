using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IUnitRepository
    {
        Task<List<Unit>> GetAllAsync();
        Task<Unit> GetByIdAsync(int id);
        Task<Unit> CreateAsync(Unit unit);
        Task<Unit> UpdateAsync(Unit unit);
        Task<Unit> DeleteAsync(int id);
        Task<Unit> RestoreAsync(int id);
    }
}
