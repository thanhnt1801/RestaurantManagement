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
    public class GuestTablesController : ControllerBase
    {
        private readonly IGuestTableRepository _guestTableRepository;
        private readonly IMapper _mapper;

        public GuestTablesController(IGuestTableRepository guestTableRepository, IMapper mapper)
        {
            _guestTableRepository = guestTableRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<GuestTableDTO>> GetAllGuestTable()
        {
            var getAllGuestTable = await _guestTableRepository.GetAllAsync();
            return _mapper.Map<List<GuestTableDTO>>(getAllGuestTable);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestTableDTO>> GetGuestTableById(int id)
        {
            var getGuestTable = await _guestTableRepository.GetByIdAsync(id);
            if (getGuestTable == null) return BadRequest("GuestTable is not existed!");
            return _mapper.Map<GuestTableDTO>(getGuestTable);
        }

        [HttpPost]
        public async Task<ActionResult<GuestTableDTO>> CreateGuestTable(GuestTableDTO guestTable)
        {
            var newGuestTable = await _guestTableRepository.CreateAsync(_mapper.Map<GuestTable>(guestTable));
            return _mapper.Map<GuestTableDTO>(newGuestTable);
        }

        [HttpPut]
        public async Task<ActionResult<GuestTableDTO>> UpdateGuestTable(GuestTableDTO guestTable)
        {
            if (await _guestTableRepository.GetByIdAsync(guestTable.Id) == null) return BadRequest("GuestTable is not existed!");
            var updatedGuestTable = await _guestTableRepository.UpdateAsync(_mapper.Map<GuestTable>(guestTable));
            return _mapper.Map<GuestTableDTO>(updatedGuestTable);
        }

        [HttpPut("RestoreGuestTable/{id}")]
        public async Task<ActionResult<GuestTableDTO>> RestoreGuestTable(int id)
        {
            if (await _guestTableRepository.GetByIdAsync(id) == null) return BadRequest("GuestTable is not existed!");
            var restoredGuestTable = await _guestTableRepository.RestoreAsync(id);
            return _mapper.Map<GuestTableDTO>(restoredGuestTable);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GuestTableDTO>> DeleteGuestTable(int id)
        {
            if (await _guestTableRepository.GetByIdAsync(id) == null) return BadRequest("GuestTable is not existed!");
            var deletedGuestTable = await _guestTableRepository.DeleteAsync(id);
            return _mapper.Map<GuestTableDTO>(deletedGuestTable);
        }
    }
}
