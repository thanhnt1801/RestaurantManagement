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
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantsController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<RestaurantDTO>> GetAllRestaurant()
        {
            var getAllRestaurant = await _restaurantRepository.GetAllAsync();
            return _mapper.Map<List<RestaurantDTO>>(getAllRestaurant);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurantById(int id)
        {
            var getRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (getRestaurant == null) return BadRequest("Restaurant is not existed!");
            return _mapper.Map<RestaurantDTO>(getRestaurant);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDTO>> CreateRestaurant(RestaurantDTO restaurant)
        {
            var newRestaurant = await _restaurantRepository.CreateAsync(_mapper.Map<Restaurant>(restaurant));
            return _mapper.Map<RestaurantDTO>(newRestaurant);
        }

        [HttpPut]
        public async Task<ActionResult<RestaurantDTO>> UpdateRestaurant(RestaurantDTO restaurant)
        {
            if (await _restaurantRepository.GetByIdAsync(restaurant.Id) == null) return BadRequest("Restaurant is not existed!");
            var updatedRestaurant = await _restaurantRepository.UpdateAsync(_mapper.Map<Restaurant>(restaurant));
            return _mapper.Map<RestaurantDTO>(updatedRestaurant);
        }

        [HttpPut("RestoreRestaurant/{id}")]
        public async Task<ActionResult<RestaurantDTO>> RestoreRestaurant(int id)
        {
            if (await _restaurantRepository.GetByIdAsync(id) == null) return BadRequest("Restaurant is not existed!");
            var restoredRestaurant = await _restaurantRepository.RestoreAsync(id);
            return _mapper.Map<RestaurantDTO>(restoredRestaurant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RestaurantDTO>> DeleteRestaurant(int id)
        {
            if (await _restaurantRepository.GetByIdAsync(id) == null) return BadRequest("Restaurant is not existed!");
            var deletedRestaurant = await _restaurantRepository.DeleteAsync(id);
            return _mapper.Map<RestaurantDTO>(deletedRestaurant);
        }
    }
}
