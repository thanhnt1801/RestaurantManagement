using QLNH_Client.DTOs;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
        Task<Role> CreateAsync(Role role);
        Task<Role> UpdateAsync(Role role);
        Task<Role> DeleteAsync(int id);
        Task<Role> RestoreAsync(int id);
    }
}
