using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAllAsync();
        Task<Restaurant> GetByIdAsync(int id);
        Task<Restaurant> CreateAsync(Restaurant restaurant);
        Task<Restaurant> UpdateAsync(Restaurant restaurant);
        Task<Restaurant> DeleteAsync(int id);
        Task<Restaurant> RestoreAsync(int id);
    }
}
