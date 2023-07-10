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
    public class GuestsController : ControllerBase
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public GuestsController(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<GuestDTO>> GetAllGuest()
        {
            var getAllGuest = await _guestRepository.GetAllAsync();
            return _mapper.Map<List<GuestDTO>>(getAllGuest);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestDTO>> GetGuestById(int id)
        {
            var getGuest = await _guestRepository.GetByIdAsync(id);
            if (getGuest == null) return BadRequest("Guest is not existed!");
            return _mapper.Map<GuestDTO>(getGuest);
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> CreateGuest(GuestDTO guest)
        {
            var newGuest = await _guestRepository.CreateAsync(_mapper.Map<Guest>(guest));
            return _mapper.Map<GuestDTO>(newGuest);
        }

        [HttpPut]
        public async Task<ActionResult<GuestDTO>> UpdateGuest(GuestDTO guest)
        {
            if (await _guestRepository.GetByIdAsync(guest.Id) == null) return BadRequest("Guest is not existed!");
            var updatedGuest = await _guestRepository.UpdateAsync(_mapper.Map<Guest>(guest));
            return _mapper.Map<GuestDTO>(updatedGuest);
        }

        [HttpPut("RestoreGuest/{id}")]
        public async Task<ActionResult<GuestDTO>> RestoreGuest(int id)
        {
            if (await _guestRepository.GetByIdAsync(id) == null) return BadRequest("Guest is not existed!");
            var restoredGuest = await _guestRepository.RestoreAsync(id);
            return _mapper.Map<GuestDTO>(restoredGuest);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GuestDTO>> DeleteGuest(int id)
        {
            if (await _guestRepository.GetByIdAsync(id) == null) return BadRequest("Guest is not existed!");
            var deletedGuest = await _guestRepository.DeleteAsync(id);
            return _mapper.Map<GuestDTO>(deletedGuest);
        }
    }
}
