using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;
using System.Data;

namespace QLNH_Client.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            try
            {
                var getRole = await GetByIdAsync(role.Id);
                var newRole = new Role
                {
                    Name = role.Name,
                    Description = role.Description,
                    Created = role.Created,
                    Updated = role.Updated,
                    Deleted = role.Deleted,
                };
                await _context.AddAsync(newRole);
                await _context.SaveChangesAsync();
                return newRole;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> DeleteAsync(int id)
        {
            try
            {
                var getRole = await GetByIdAsync(id);
                getRole.Deleted = true;
                _context.Update(getRole);
                await _context.SaveChangesAsync();
                return getRole;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Role>> GetAllAsync()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> RestoreAsync(int id)
        {
            try
            {
                var getRole = await GetByIdAsync(id);
                getRole.Deleted = false;
                _context.Update(getRole);
                await _context.SaveChangesAsync();
                return getRole;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> UpdateAsync(Role role)
        {
            try
            {
                var getRole = await GetByIdAsync(role.Id);
                getRole.Name = role.Name;
                getRole.Description = role.Description;
                getRole.Updated = role.Updated;
                _context.Update(getRole);
                await _context.SaveChangesAsync();
                return getRole;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
