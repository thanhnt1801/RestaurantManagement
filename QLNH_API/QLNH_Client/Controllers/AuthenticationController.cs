using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.DTOs;
using QLNH_Client.Models;
using QLNH_Client.Repositories;
using QLNH_Client.Services;
using System.Security.Cryptography;

namespace QLNH_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenSerivce _tokenSerivce;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthenticationController(DataContext context, 
            ITokenSerivce tokenSerivce,
            IRestaurantRepository restaurantRepository,
            IRoleRepository roleRepository)
        {
            _context = context;
            _tokenSerivce = tokenSerivce;
            _restaurantRepository = restaurantRepository;
            _roleRepository = roleRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AccountDTO>> Register(RegisterDTO registerDTO)
        {
            if (_context.Users.Any(u => u.UserName == registerDTO.UserName))
            {
                return BadRequest("User already exists.");
            }

            CreatePasswordHash(registerDTO.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            if(await _restaurantRepository.GetByIdAsync(registerDTO.RestaurantId) == null) return BadRequest("Restaurant does not exists.");
            if (await _roleRepository.GetByIdAsync(registerDTO.RoleId) == null) return BadRequest("Role does not exists.");

            var user = new User
            {
                UserName = registerDTO.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = registerDTO.RoleId,
                RestaurantId = registerDTO.RestaurantId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AccountDTO { UserName = registerDTO.UserName, Token = _tokenSerivce.CreateToken(user) };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccountDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect.");
            }

            return new AccountDTO { UserName = loginDTO.UserName, Token = _tokenSerivce.CreateToken(user) };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
