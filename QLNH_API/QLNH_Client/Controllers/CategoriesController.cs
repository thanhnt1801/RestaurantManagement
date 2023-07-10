using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNH_Client.DTOs;
using QLNH_Client.Models;
using QLNH_Client.Repositories;

namespace QLNH_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            var getAllCategory = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDTO>>(getAllCategory);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var getCategory = await _categoryRepository.GetByIdAsync(id);
            if (getCategory == null) return BadRequest("Category is not existed!");
            return _mapper.Map<CategoryDTO>(getCategory);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO category)
        {
            if(category.ParentId == 0)
            {
                category.ParentId = null;
            }
            var newCategory = await _categoryRepository.CreateAsync(_mapper.Map<Category>(category));
            return _mapper.Map<CategoryDTO>(newCategory);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(CategoryDTO category)
        {
            if (await _categoryRepository.GetByIdAsync(category.Id) == null) return BadRequest("Category is not existed!");
            var updatedCategory = await _categoryRepository.UpdateAsync(_mapper.Map<Category>(category));
            return _mapper.Map<CategoryDTO>(updatedCategory);
        }

        [HttpPut("RestoreCategory/{id}")]
        public async Task<ActionResult<CategoryDTO>> RestoreCategory(int id)
        {
            if (await _categoryRepository.GetByIdAsync(id) == null) return BadRequest("Category is not existed!");
            var restoredCategory = await _categoryRepository.RestoreAsync(id);
            return _mapper.Map<CategoryDTO>(restoredCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> DeleteCategory(int id)
        {
            if (await _categoryRepository.GetByIdAsync(id) == null) return BadRequest("Category is not existed!");
            var deletedCategory = await _categoryRepository.DeleteAsync(id);
            return _mapper.Map<CategoryDTO>(deletedCategory);
        }
    }
}
