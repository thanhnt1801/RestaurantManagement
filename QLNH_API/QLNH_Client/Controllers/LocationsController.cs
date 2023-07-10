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
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationsController(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<LocationDTO>> GetAllLocation()
        {
            var getAllLocation = await _locationRepository.GetAllAsync();
            return _mapper.Map<List<LocationDTO>>(getAllLocation);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocationById(int id)
        {
            var getLocation = await _locationRepository.GetByIdAsync(id);
            if (getLocation == null) return BadRequest("Location is not existed!");
            return _mapper.Map<LocationDTO>(getLocation);
        }

        [HttpPost]
        public async Task<ActionResult<LocationDTO>> CreateLocation(LocationDTO location)
        {
            var newLocation = await _locationRepository.CreateAsync(_mapper.Map<Location>(location));
            return _mapper.Map<LocationDTO>(newLocation);
        }

        [HttpPut]
        public async Task<ActionResult<LocationDTO>> UpdateLocation(LocationDTO location)
        {
            if (await _locationRepository.GetByIdAsync(location.Id) == null) return BadRequest("Location is not existed!");
            var updatedLocation = await _locationRepository.UpdateAsync(_mapper.Map<Location>(location));
            return _mapper.Map<LocationDTO>(updatedLocation);
        }

        [HttpPut("RestoreLocation/{id}")]
        public async Task<ActionResult<LocationDTO>> RestoreLocation(int id)
        {
            if (await _locationRepository.GetByIdAsync(id) == null) return BadRequest("Location is not existed!");
            var restoredLocation = await _locationRepository.RestoreAsync(id);
            return _mapper.Map<LocationDTO>(restoredLocation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LocationDTO>> DeleteLocation(int id)
        {
            if (await _locationRepository.GetByIdAsync(id) == null) return BadRequest("Location is not existed!");
            var deletedLocation = await _locationRepository.DeleteAsync(id);
            return _mapper.Map<LocationDTO>(deletedLocation);
        }
    }
}
