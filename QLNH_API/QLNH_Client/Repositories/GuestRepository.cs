using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly DataContext _context;

        public GuestRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Guest> CreateAsync(Guest guest)
        {
            try
            {
                var getGuest = await GetByIdAsync(guest.Id);
                var newGuest = new Guest
                {
                    Name = guest.Name,
                    Description = guest.Description,
                    Created = guest.Created,
                    Updated = guest.Updated,
                    Deleted = guest.Deleted,
                    Phone = guest.Phone,
                    CreatedById = guest.CreatedById,
                    UpdatedById = guest.UpdatedById,
                    RestaurantId = guest.RestaurantId
                };
                await _context.AddAsync(newGuest);
                await _context.SaveChangesAsync();
                return newGuest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guest> DeleteAsync(int id)
        {
            try
            {
                var getGuest = await GetByIdAsync(id);
                getGuest.Deleted = true;
                _context.Update(getGuest);
                await _context.SaveChangesAsync();
                return getGuest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Guest>> GetAllAsync()
        {
            try
            {
                return await _context.Guests.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guest> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Guests.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guest> RestoreAsync(int id)
        {
            try
            {
                var getGuest = await GetByIdAsync(id);
                getGuest.Deleted = false;
                _context.Update(getGuest);
                await _context.SaveChangesAsync();
                return getGuest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guest> UpdateAsync(Guest guest)
        {
            try
            {
                var getGuest = await GetByIdAsync(guest.Id);
                getGuest.Name = guest.Name;
                getGuest.Description = guest.Description;
                getGuest.Updated = guest.Updated;
                getGuest.Phone = guest.Phone;
                getGuest.UpdatedById = guest.UpdatedById;
                _context.Update(getGuest);
                await _context.SaveChangesAsync();
                return getGuest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
