using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetAsync(int id);
        Task<User> UpdateAsync(User user);
        Task<User> DeleteAsync(int id);
        Task<User> RestoreAsync(int id);
        Task<bool> AddUserToRoleAsync(int roleId, int userId);
        Task<bool> AddUserToRestaurantAsync(int restaurantId, int userId);

    }
}
