using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Item> CreateAsync(Item item)
        {
            try
            {
                var getItem = await GetByIdAsync(item.Id);
                var newItem = new Item
                {
                    Name = item.Name,
                    Description = item.Description,
                    Created = item.Created,
                    Updated = item.Updated,
                    Deleted = item.Deleted,
                    Discount = item.Discount,
                    RestaurantId = item.RestaurantId,
                    CategoryId = item.CategoryId,
                };
                await _context.AddAsync(newItem);
                await _context.SaveChangesAsync();
                return newItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Item> DeleteAsync(int id)
        {
            try
            {
                var getItem = await GetByIdAsync(id);
                getItem.Deleted = true;
                _context.Update(getItem);
                await _context.SaveChangesAsync();
                return getItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Item>> GetAllAsync()
        {
            try
            {
                return await _context.Items
                    .Include(r => r.Restaurant)
                    .Include(p => p.Prices).ThenInclude(s => s.Size)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Items.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Item> RestoreAsync(int id)
        {
            try
            {
                var getItem = await GetByIdAsync(id);
                getItem.Deleted = false;
                _context.Update(getItem);
                await _context.SaveChangesAsync();
                return getItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            try
            {
                var getItem = await GetByIdAsync(item.Id);
                getItem.Name = item.Name;
                getItem.Description = item.Description;
                getItem.Updated = item.Updated;
                getItem.Discount = item.Discount;
                getItem.CategoryId = item.CategoryId;
                _context.Update(getItem);
                await _context.SaveChangesAsync();
                return getItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
