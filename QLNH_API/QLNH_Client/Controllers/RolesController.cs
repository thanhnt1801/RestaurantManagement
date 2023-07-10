using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNH_Client.Data;
using QLNH_Client.DTOs;
using QLNH_Client.Models;
using QLNH_Client.Repositories;
using System.Data;

namespace QLNH_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RolesController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<RoleDTO>> GetAllRole()
        {
            var getAllRole = await _roleRepository.GetAllAsync();
            return _mapper.Map<List<RoleDTO>>(getAllRole);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetRoleById(int id)
        {
            var getRole = await _roleRepository.GetByIdAsync(id);
            if(getRole == null) return BadRequest("Role is not existed!");
            return _mapper.Map<RoleDTO>(getRole);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> CreateRole(RoleDTO role)
        {
            var newRole = await _roleRepository.CreateAsync(_mapper.Map<Role>(role));
            return _mapper.Map<RoleDTO>(newRole);
        }

        [HttpPut]
        public async Task<ActionResult<RoleDTO>> UpdateRole(RoleDTO role)
        {
            if (await _roleRepository.GetByIdAsync(role.Id) == null) return BadRequest("Role is not existed!");
            var updatedRole = await _roleRepository.UpdateAsync(_mapper.Map<Role>(role));
            return _mapper.Map<RoleDTO>(updatedRole);
        }

        [HttpPut("RestoreRole/{id}")]
        public async Task<ActionResult<RoleDTO>> RestoreRole(int id)
        {
            if (await _roleRepository.GetByIdAsync(id) == null) return BadRequest("Role is not existed!");
            var restoredRole = await _roleRepository.RestoreAsync(id);
            return _mapper.Map<RoleDTO>(restoredRole);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RoleDTO>> DeleteRole(int id)
        {
            if (await _roleRepository.GetByIdAsync(id) == null) return BadRequest("Role is not existed!");
            var deletedRole = await _roleRepository.DeleteAsync(id);
            return _mapper.Map<RoleDTO>(deletedRole);
        }
    }
}
