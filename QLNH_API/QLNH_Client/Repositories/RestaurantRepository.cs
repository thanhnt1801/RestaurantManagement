using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataContext _context;

        public RestaurantRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            try
            {
                var tempUser = await _context.Users.FindAsync(2);
                var newRestaurant = new Restaurant
                {
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    Created = restaurant.Created,
                    Updated = restaurant.Updated,
                    Deleted = restaurant.Deleted,
                    Phone = restaurant.Phone,
                    Address = restaurant.Address,
                    CreatedUser = tempUser,
                    UpdatedUser = tempUser,
                };
                await _context.AddAsync(newRestaurant);
                await _context.SaveChangesAsync();
                return newRestaurant;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Restaurant> DeleteAsync(int id)
        {
            try
            {
                var getRestaurant = await GetByIdAsync(id);
                getRestaurant.Deleted = true;
                _context.Update(getRestaurant);
                await _context.SaveChangesAsync();
                return getRestaurant;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            try
            {
                return await _context.Restaurants
                    .Include(u => u.CreatedUser)
                    .Include(u => u.UpdatedUser)
                    .Include(u => u.Users)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Restaurants
                    .Include(u => u.CreatedUser)
                    .Include(u => u.UpdatedUser)
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Restaurant> RestoreAsync(int id)
        {
            try
            {
                var getRestaurant = await GetByIdAsync(id);
                getRestaurant.Deleted = false;
                _context.Update(getRestaurant);
                await _context.SaveChangesAsync();
                return getRestaurant;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Restaurant> UpdateAsync(Restaurant restaurant)
        {
            try
            {
                var tempUser = await _context.Users.FindAsync(2);

                var getRestaurant = await GetByIdAsync(restaurant.Id);
                getRestaurant.Name = restaurant.Name;
                getRestaurant.Description = restaurant.Description;
                getRestaurant.Updated = restaurant.Updated;
                getRestaurant.Phone = restaurant.Phone;
                getRestaurant.Address = restaurant.Address;
                getRestaurant.UpdatedUser = tempUser;
                _context.Update(getRestaurant);
                await _context.SaveChangesAsync();
                return getRestaurant;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
