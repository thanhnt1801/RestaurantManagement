using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DataContext _context;

        public StatusRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Status> CreateAsync(Status status)
        {
            try
            {
                var getStatus = await GetByIdAsync(status.Id);
                var newStatus = new Status
                {
                    Name = status.Name,
                    Description = status.Description,
                    Created = status.Created,
                    Updated = status.Updated,
                    Deleted = status.Deleted,
                    RestaurantId = status.RestaurantId,
                };
                await _context.AddAsync(newStatus);
                await _context.SaveChangesAsync();
                return newStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Status> DeleteAsync(int id)
        {
            try
            {
                var getStatus = await GetByIdAsync(id);
                getStatus.Deleted = true;
                _context.Update(getStatus);
                await _context.SaveChangesAsync();
                return getStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Status>> GetAllAsync()
        {
            try
            {
                return await _context.Statuses.Include(g => g.GuestTables).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Status> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Statuses.Include(g => g.GuestTables).FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Status> RestoreAsync(int id)
        {
            try
            {
                var getStatus = await GetByIdAsync(id);
                getStatus.Deleted = false;
                _context.Update(getStatus);
                await _context.SaveChangesAsync();
                return getStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Status> UpdateAsync(Status status)
        {
            try
            {
                var getStatus = await GetByIdAsync(status.Id);
                getStatus.Name = status.Name;
                getStatus.Description = status.Description;
                getStatus.Updated = status.Updated;
                _context.Update(getStatus);
                await _context.SaveChangesAsync();
                return getStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
