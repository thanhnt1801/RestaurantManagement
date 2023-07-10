using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllAsync();
        Task<Status> GetByIdAsync(int id);
        Task<Status> CreateAsync(Status status);
        Task<Status> UpdateAsync(Status status);
        Task<Status> DeleteAsync(int id);
        Task<Status> RestoreAsync(int id);
    }
}
