using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class GuestTableRepository : IGuestTableRepository
    {
        private readonly DataContext _context;

        public GuestTableRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<GuestTable> CreateAsync(GuestTable guestTable)
        {
            try
            {
                var getGuestTable = await GetByIdAsync(guestTable.Id);
                var newGuestTable = new GuestTable
                {
                    Name = guestTable.Name,
                    Description = guestTable.Description,
                    Created = guestTable.Created,
                    Updated = guestTable.Updated,
                    Deleted = guestTable.Deleted,
                    RestaurantId = guestTable.RestaurantId,
                    LocationId = guestTable.LocationId,
                    StatusId = guestTable.StatusId,
                };
                await _context.AddAsync(newGuestTable);
                await _context.SaveChangesAsync();
                return newGuestTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GuestTable> DeleteAsync(int id)
        {
            try
            {
                var getGuestTable = await GetByIdAsync(id);
                getGuestTable.Deleted = true;
                _context.Update(getGuestTable);
                await _context.SaveChangesAsync();
                return getGuestTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GuestTable>> GetAllAsync()
        {
            try
            {
                return await _context.GuestTables
                    .Include(l => l.Location)
                    .Include(s => s.Status)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GuestTable> GetByIdAsync(int id)
        {
            try
            {
                return await _context.GuestTables.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GuestTable> RestoreAsync(int id)
        {
            try
            {
                var getGuestTable = await GetByIdAsync(id);
                getGuestTable.Deleted = false;
                _context.Update(getGuestTable);
                await _context.SaveChangesAsync();
                return getGuestTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GuestTable> UpdateAsync(GuestTable guestTable)
        {
            try
            {
                var getGuestTable = await GetByIdAsync(guestTable.Id);
                getGuestTable.Name = guestTable.Name;
                getGuestTable.Description = guestTable.Description;
                getGuestTable.Updated = guestTable.Updated;
                getGuestTable.LocationId = guestTable.LocationId;
                getGuestTable.StatusId = guestTable.StatusId;
                _context.Update(getGuestTable);
                await _context.SaveChangesAsync();
                return getGuestTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
