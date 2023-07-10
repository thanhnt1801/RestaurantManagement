using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly DataContext _context;

        public UnitRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> CreateAsync(Unit unit)
        {
            try
            {
                var getUnit = await GetByIdAsync(unit.Id);
                var newUnit = new Unit
                {
                    Name = unit.Name,
                    Description = unit.Description,
                    Created = unit.Created,
                    Updated = unit.Updated,
                    Deleted = unit.Deleted,
                    RestaurantId = unit.RestaurantId
                };
                await _context.AddAsync(newUnit);
                await _context.SaveChangesAsync();
                return newUnit;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Unit> DeleteAsync(int id)
        {
            try
            {
                var getUnit = await GetByIdAsync(id);
                getUnit.Deleted = true;
                _context.Update(getUnit);
                await _context.SaveChangesAsync();
                return getUnit;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Unit>> GetAllAsync()
        {
            try
            {
                return await _context.Units
                    .Include(s => s.Sizes)
                    .Include(p => p.Prices)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Unit> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Units
                    .Include(s => s.Sizes)
                    .Include(p => p.Prices)
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Unit> RestoreAsync(int id)
        {
            try
            {
                var getUnit = await GetByIdAsync(id);
                getUnit.Deleted = false;
                _context.Update(getUnit);
                await _context.SaveChangesAsync();
                return getUnit;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Unit> UpdateAsync(Unit unit)
        {
            try
            {
                var getUnit = await GetByIdAsync(unit.Id);
                getUnit.Name = unit.Name;
                getUnit.Description = unit.Description;
                getUnit.Updated = unit.Updated;
                _context.Update(getUnit);
                await _context.SaveChangesAsync();
                return getUnit;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
