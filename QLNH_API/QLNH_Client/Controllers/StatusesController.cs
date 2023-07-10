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
    public class StatusesController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusesController(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<StatusDTO>> GetAllStatus()
        {
            var getAllStatus = await _statusRepository.GetAllAsync();
            return _mapper.Map<List<StatusDTO>>(getAllStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDTO>> GetStatusById(int id)
        {
            var getStatus = await _statusRepository.GetByIdAsync(id);
            if (getStatus == null) return BadRequest("Status is not existed!");
            return _mapper.Map<StatusDTO>(getStatus);
        }

        [HttpPost]
        public async Task<ActionResult<StatusDTO>> CreateStatus(StatusDTO status)
        {
            var newStatus = await _statusRepository.CreateAsync(_mapper.Map<Status>(status));
            return _mapper.Map<StatusDTO>(newStatus);
        }

        [HttpPut]
        public async Task<ActionResult<StatusDTO>> UpdateStatus(StatusDTO status)
        {
            if (await _statusRepository.GetByIdAsync(status.Id) == null) return BadRequest("Status is not existed!");
            var updatedStatus = await _statusRepository.UpdateAsync(_mapper.Map<Status>(status));
            return _mapper.Map<StatusDTO>(updatedStatus);
        }

        [HttpPut("RestoreStatus/{id}")]
        public async Task<ActionResult<StatusDTO>> RestoreStatus(int id)
        {
            if (await _statusRepository.GetByIdAsync(id) == null) return BadRequest("Status is not existed!");
            var restoredStatus = await _statusRepository.RestoreAsync(id);
            return _mapper.Map<StatusDTO>(restoredStatus);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StatusDTO>> DeleteStatus(int id)
        {
            if (await _statusRepository.GetByIdAsync(id) == null) return BadRequest("Status is not existed!");
            var deletedStatus = await _statusRepository.DeleteAsync(id);
            return _mapper.Map<StatusDTO>(deletedStatus);
        }
    }
}
