using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly DataContext _context;

        public PriceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Price> CreateAsync(Price price)
        {
            try
            {
                var getPrice = await GetByIdAsync(price.Id);
                var newPrice = new Price
                {
                    Description = price.Description,
                    Created = price.Created,
                    Updated = price.Updated,
                    Deleted = price.Deleted,
                    SalePrice = price.SalePrice,
                    UnitId = price.UnitId,
                    RestaurantId = price.RestaurantId,
                    SizeId = price.SizeId,
                    ItemId = price.ItemId,
                };
                await _context.AddAsync(newPrice);
                await _context.SaveChangesAsync();
                return newPrice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Price> DeleteAsync(int id)
        {
            try
            {
                var getPrice = await GetByIdAsync(id);
                getPrice.Deleted = true;
                _context.Update(getPrice);
                await _context.SaveChangesAsync();
                return getPrice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Price>> GetAllAsync()
        {
            try
            {
                return await _context.Prices
                    .Include(s => s.Size)
                    .Include(u => u.Unit)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Price> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Prices
                    .Include(s => s.Size)
                    .Include(u => u.Unit)
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Price> RestoreAsync(int id)
        {
            try
            {
                var getPrice = await GetByIdAsync(id);
                getPrice.Deleted = false;
                _context.Update(getPrice);
                await _context.SaveChangesAsync();
                return getPrice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Price> UpdateAsync(Price price)
        {
            try
            {
                var getPrice = await GetByIdAsync(price.Id);
                getPrice.Description = price.Description;
                getPrice.Updated = price.Updated;
                getPrice.UnitId = price.UnitId;
                getPrice.SizeId = price.SizeId;
                getPrice.ItemId = price.ItemId;
                getPrice.SalePrice = price.SalePrice;

                _context.Update(getPrice);
                await _context.SaveChangesAsync();
                return getPrice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
