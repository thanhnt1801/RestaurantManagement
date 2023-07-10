using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IItemImageRepository
    {
        Task<List<ItemImage>> GetAllAsync();
        Task<ItemImage> GetByIdAsync(int id);
        Task<ItemImage> CreateAsync(ItemImage itemImage);
        Task<ItemImage> UpdateAsync(ItemImage itemImage);
        Task<ItemImage> DeleteAsync(int id);
        Task<ItemImage> RestoreAsync(int id);
    }
}
