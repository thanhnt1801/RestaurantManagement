using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class ItemImageRepository : IItemImageRepository
    {
        private readonly DataContext _context;

        public ItemImageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ItemImage> CreateAsync(ItemImage itemImage)
        {
            try
            {
                var getItemImage = await GetByIdAsync(itemImage.Id);
                var newItemImage = new ItemImage
                {
                    Name = itemImage.Name,
                    Description = itemImage.Description,
                    Created = itemImage.Created,
                    Updated = itemImage.Updated,
                    Deleted = itemImage.Deleted,
                    RestaurantId = itemImage.RestaurantId,
                    ItemId = itemImage.ItemId,
                    Base64 = itemImage.Base64
                };
                await _context.AddAsync(newItemImage);
                await _context.SaveChangesAsync();
                return newItemImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemImage> DeleteAsync(int id)
        {
            try
            {
                var getItemImage = await GetByIdAsync(id);
                getItemImage.Deleted = true;
                _context.Update(getItemImage);
                await _context.SaveChangesAsync();
                return getItemImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ItemImage>> GetAllAsync()
        {
            try
            {
                return await _context.ItemImages.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemImage> GetByIdAsync(int id)
        {
            try
            {
                return await _context.ItemImages.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemImage> RestoreAsync(int id)
        {
            try
            {
                var getItemImage = await GetByIdAsync(id);
                getItemImage.Deleted = false;
                _context.Update(getItemImage);
                await _context.SaveChangesAsync();
                return getItemImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemImage> UpdateAsync(ItemImage itemImage)
        {
            try
            {
                var getItemImage = await GetByIdAsync(itemImage.Id);
                getItemImage.Name = itemImage.Name;
                getItemImage.Base64 = itemImage.Base64;
                getItemImage.Description = itemImage.Description;
                getItemImage.Updated = itemImage.Updated;
                getItemImage.ItemId = itemImage.ItemId;
                _context.Update(getItemImage);
                await _context.SaveChangesAsync();
                return getItemImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
