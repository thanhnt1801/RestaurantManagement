using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(r => r.Role)
                .Include(res => res.Restaurant)
                .ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users
                .Include(r => r.Role)
                .Include(res => res.Restaurant)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> DeleteAsync(int id)
        {
            try
            {
                var getUser = await GetAsync(id);
                getUser.Deleted = true;
                _context.Update(getUser);
                await _context.SaveChangesAsync();
                return getUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User> RestoreAsync(int id)
        {
            try
            {
                var getUser = await GetAsync(id);
                getUser.Deleted = false;
                _context.Update(getUser);
                await _context.SaveChangesAsync();
                return getUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            try
            {
                var getUser = await GetAsync(user.Id);
                getUser.Description = user.Description;
                getUser.UpdatedAt = user.UpdatedAt;
                getUser.OffDuty = user.OffDuty;
                _context.Update(getUser);
                await _context.SaveChangesAsync();
                return getUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddUserToRoleAsync(int roleId, int userId)
        {
            try
            {
                var getRole = await _context.Roles.FirstOrDefaultAsync(u => u.Id == roleId);
                var getUser = await GetAsync(userId);

                getUser.Role = getRole;

                _context.Update(getUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddUserToRestaurantAsync(int restaurantId, int userId)
        {
            try
            {
                var getRestaurant = await _context.Restaurants.FirstOrDefaultAsync(u => u.Id == restaurantId);
                var getUser = await GetAsync(userId);

                getUser.Restaurant = getRestaurant;

                _context.Update(getUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
