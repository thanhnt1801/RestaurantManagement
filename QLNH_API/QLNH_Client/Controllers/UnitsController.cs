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
    public class UnitsController : ControllerBase
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public UnitsController(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<UnitDTO>> GetAllUnit()
        {
            var getAllUnit = await _unitRepository.GetAllAsync();
            return _mapper.Map<List<UnitDTO>>(getAllUnit);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitDTO>> GetUnitById(int id)
        {
            var getUnit = await _unitRepository.GetByIdAsync(id);
            if (getUnit == null) return BadRequest("Unit is not existed!");
            return _mapper.Map<UnitDTO>(getUnit);
        }

        [HttpPost]
        public async Task<ActionResult<UnitDTO>> CreateUnit(UnitDTO unit)
        {
            var newUnit = await _unitRepository.CreateAsync(_mapper.Map<Unit>(unit));
            return _mapper.Map<UnitDTO>(newUnit);
        }

        [HttpPut]
        public async Task<ActionResult<UnitDTO>> UpdateUnit(UnitDTO unit)
        {
            if (await _unitRepository.GetByIdAsync(unit.Id) == null) return BadRequest("Unit is not existed!");
            var updatedUnit = await _unitRepository.UpdateAsync(_mapper.Map<Unit>(unit));
            return _mapper.Map<UnitDTO>(updatedUnit);
        }

        [HttpPut("RestoreUnit/{id}")]
        public async Task<ActionResult<UnitDTO>> RestoreUnit(int id)
        {
            if (await _unitRepository.GetByIdAsync(id) == null) return BadRequest("Unit is not existed!");
            var restoredUnit = await _unitRepository.RestoreAsync(id);
            return _mapper.Map<UnitDTO>(restoredUnit);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UnitDTO>> DeleteUnit(int id)
        {
            if (await _unitRepository.GetByIdAsync(id) == null) return BadRequest("Unit is not existed!");
            var deletedUnit = await _unitRepository.DeleteAsync(id);
            return _mapper.Map<UnitDTO>(deletedUnit);
        }
    }
}
