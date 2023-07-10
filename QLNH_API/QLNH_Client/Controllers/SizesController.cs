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
    public class SizesController : ControllerBase
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public SizesController(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<SizeDTO>> GetAllSize()
        {
            var getAllSize = await _sizeRepository.GetAllAsync();
            return _mapper.Map<List<SizeDTO>>(getAllSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SizeDTO>> GetSizeById(int id)
        {
            var getSize = await _sizeRepository.GetByIdAsync(id);
            if (getSize == null) return BadRequest("Size is not existed!");
            return _mapper.Map<SizeDTO>(getSize);
        }

        [HttpPost]
        public async Task<ActionResult<SizeDTO>> CreateSize(SizeDTO size)
        {
            var newSize = await _sizeRepository.CreateAsync(_mapper.Map<Size>(size));
            return _mapper.Map<SizeDTO>(newSize);
        }

        [HttpPut]
        public async Task<ActionResult<SizeDTO>> UpdateSize(SizeDTO size)
        {
            if (await _sizeRepository.GetByIdAsync(size.Id) == null) return BadRequest("Size is not existed!");
            var updatedSize = await _sizeRepository.UpdateAsync(_mapper.Map<Size>(size));
            return _mapper.Map<SizeDTO>(updatedSize);
        }

        [HttpPut("RestoreSize/{id}")]
        public async Task<ActionResult<SizeDTO>> RestoreSize(int id)
        {
            if (await _sizeRepository.GetByIdAsync(id) == null) return BadRequest("Size is not existed!");
            var restoredSize = await _sizeRepository.RestoreAsync(id);
            return _mapper.Map<SizeDTO>(restoredSize);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SizeDTO>> DeleteSize(int id)
        {
            if (await _sizeRepository.GetByIdAsync(id) == null) return BadRequest("Size is not existed!");
            var deletedSize = await _sizeRepository.DeleteAsync(id);
            return _mapper.Map<SizeDTO>(deletedSize);
        }
    }
}
