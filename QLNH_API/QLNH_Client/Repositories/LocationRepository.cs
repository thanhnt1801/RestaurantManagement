using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext _context;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Location> CreateAsync(Location location)
        {
            try
            {
                var getLocation = await GetByIdAsync(location.Id);
                var newLocation = new Location
                {
                    Name = location.Name,
                    Description = location.Description,
                    Created = location.Created,
                    Updated = location.Updated,
                    Deleted = location.Deleted,
                    RestaurantId = location.RestaurantId
                };
                await _context.AddAsync(newLocation);
                await _context.SaveChangesAsync();
                return newLocation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Location> DeleteAsync(int id)
        {
            try
            {
                var getLocation = await GetByIdAsync(id);
                getLocation.Deleted = true;
                _context.Update(getLocation);
                await _context.SaveChangesAsync();
                return getLocation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Location>> GetAllAsync()
        {
            try
            {
                return await _context.Locations.Include(g => g.GuestTables).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Locations.Include(g => g.GuestTables).FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Location> RestoreAsync(int id)
        {
            try
            {
                var getLocation = await GetByIdAsync(id);
                getLocation.Deleted = false;
                _context.Update(getLocation);
                await _context.SaveChangesAsync();
                return getLocation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Location> UpdateAsync(Location location)
        {
            try
            {
                var getLocation = await GetByIdAsync(location.Id);
                getLocation.Name = location.Name;
                getLocation.Description = location.Description;
                getLocation.Updated = location.Updated;
                _context.Update(getLocation);
                await _context.SaveChangesAsync();
                return getLocation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
