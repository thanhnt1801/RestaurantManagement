using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly DataContext _context;

        public SizeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Size> CreateAsync(Size size)
        {
            try
            {
                var getSize = await GetByIdAsync(size.Id);
                var newSize = new Size
                {
                    Name = size.Name,
                    Description = size.Description,
                    Created = size.Created,
                    Updated = size.Updated,
                    Deleted = size.Deleted,
                    RestaurantId = size.RestaurantId,
                    UnitId = size.UnitId,
                };
                await _context.AddAsync(newSize);
                await _context.SaveChangesAsync();
                return newSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Size> DeleteAsync(int id)
        {
            try
            {
                var getSize = await GetByIdAsync(id);
                getSize.Deleted = true;
                _context.Update(getSize);
                await _context.SaveChangesAsync();
                return getSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Size>> GetAllAsync()
        {
            try
            {
                return await _context.Sizes
                    .Include(u => u.Unit)
                    .Include(p => p.Prices).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Size> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Sizes.Include(p => p.Prices).Include(u => u.Unit).FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Size> RestoreAsync(int id)
        {
            try
            {
                var getSize = await GetByIdAsync(id);
                getSize.Deleted = false;
                _context.Update(getSize);
                await _context.SaveChangesAsync();
                return getSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Size> UpdateAsync(Size size)
        {
            try
            {
                var getSize = await GetByIdAsync(size.Id);
                getSize.Name = size.Name;
                getSize.Description = size.Description;
                getSize.Updated = size.Updated;
                getSize.UnitId = size.UnitId;
                _context.Update(getSize);
                await _context.SaveChangesAsync();
                return getSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
