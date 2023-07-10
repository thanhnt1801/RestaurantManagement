using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            try
            {
                var getCategory = await GetByIdAsync(category.Id);
                var newCategory = new Category
                {
                    Name = category.Name,
                    Description = category.Description,
                    Created = category.Created,
                    Updated = category.Updated,
                    Deleted = category.Deleted,
                    Parent = category.Parent,
                    Children = category.Children,
                    Restaurant = category.Restaurant,
                    ParentId = category.ParentId,
                    RestaurantId = category.RestaurantId,
                };
                await _context.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return newCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> DeleteAsync(int id)
        {
            try
            {
                var getCategory = await GetByIdAsync(id);
                getCategory.Deleted = true;
                _context.Update(getCategory);
                await _context.SaveChangesAsync();
                return getCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                return await _context.Categories
                    .Include(u => u.Restaurant)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Categories
                    .Include(u => u.Restaurant)
                    .Include(c => c.Children)
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> RestoreAsync(int id)
        {
            try
            {
                var getCategory = await GetByIdAsync(id);
                getCategory.Deleted = false;
                _context.Update(getCategory);
                await _context.SaveChangesAsync();
                return getCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            try
            {
                var getCategory = await GetByIdAsync(category.Id);
                getCategory.Name = category.Name;
                getCategory.Description = category.Description;
                getCategory.Updated = category.Updated;
                getCategory.ParentId = category.ParentId;
                getCategory.Parent = category.Parent;
                getCategory.Children = category.Children;
                _context.Update(getCategory);
                await _context.SaveChangesAsync();
                return getCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
