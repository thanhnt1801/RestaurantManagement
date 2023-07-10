using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.DTOs;
using QLNH_Client.Models;
using QLNH_Client.Repositories;

namespace QLNH_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public UsersController(IUserRepository userRepository, IMapper mapper,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUser()
        {
            return Ok(_mapper.Map<List<UserDTO>>(await _userRepository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            return Ok(_mapper.Map<UserDTO>(await _userRepository.GetAsync(id)));
        }

        [HttpPut("AddUserToRole")]
        public async Task<ActionResult<bool>> AddUserToRole(int roleId, int userId)
        {
            if (await _roleRepository.GetByIdAsync(roleId) == null) return BadRequest("Role is not existed!");
            var user = await _userRepository.GetAsync(userId);
            if (user == null) return BadRequest("User is not existed!");

            return await _userRepository.AddUserToRoleAsync(roleId, userId);
        }

        [HttpPut("AddUserToRestaurant")]
        public async Task<ActionResult<bool>> AddUserToRestaurant(int restaurantId, int userId)
        {
            if (await _roleRepository.GetByIdAsync(restaurantId) == null) return BadRequest("Restaurant is not existed!");
            var user = await _userRepository.GetAsync(userId);
            if (user == null) return BadRequest("User is not existed!");

            return await _userRepository.AddUserToRestaurantAsync(restaurantId, userId);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateUser(UserDTO user)
        {
            if (await _userRepository.GetAsync(user.Id) == null) return BadRequest("User is not existed!");
            var updatedUser = await _userRepository.UpdateAsync(_mapper.Map<User>(user));
            return _mapper.Map<UserDTO>(updatedUser);
        }

        [HttpPut("RestoreUser/{id}")]
        public async Task<ActionResult<UserDTO>> RestoreUser(int id)
        {
            if (await _userRepository.GetAsync(id) == null) return BadRequest("User is not existed!");
            var restoredUser = await _userRepository.RestoreAsync(id);
            return _mapper.Map<UserDTO>(restoredUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> DeleteUser(int id)
        {
            if (await _userRepository.GetAsync(id) == null) return BadRequest("User is not existed!");
            var deletedUser = await _userRepository.DeleteAsync(id);
            return _mapper.Map<UserDTO>(deletedUser);
        }
    }
}
