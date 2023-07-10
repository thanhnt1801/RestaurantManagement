using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync();
        Task<Item> GetByIdAsync(int id);
        Task<Item> CreateAsync(Item item);
        Task<Item> UpdateAsync(Item item);
        Task<Item> DeleteAsync(int id);
        Task<Item> RestoreAsync(int id);
    }
}
