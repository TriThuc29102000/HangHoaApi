using HangHoaApi.IdentityAuth;
using HangHoaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HangHoaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticateController(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerModel.Username);
            if(userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { status = "Error", message = "Người dùng đã thoát!" });
            }

            ApplicationUser user = new()
            {
                UserName = registerModel.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerModel.Email,
            };
            var resust = await _userManager.CreateAsync(user, registerModel.Password);
            if(!resust.Succeeded)
            {
              return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { status = "Error", message = "Tạo người dùng không thành công! Vui lòng kiểm tra chu hoa chu thuong và thử lại."});

            }
            return Ok(new Reponse { status = "Succesc", message = "Người dùng đã được tạo thành công!" });
        }
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerModel.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { status = "Error", message = "Người dùng đã thoát!" });
            }

            ApplicationUser user = new()
            {
                UserName = registerModel.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerModel.Email,
            };
            var resust = await _userManager.CreateAsync(user, registerModel.Password);
            if (!resust.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { status = "Error", message = "Tạo người dùng không thành công! Vui lòng kiểm tra chi tiết người dùng và thử lại." });

            }

            if (!await _roleManager.RoleExistsAsync(UserRoler.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoler.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoler.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoler.User));

            if (!await _roleManager.RoleExistsAsync(UserRoler.Admin))
                await _userManager.AddToRoleAsync(user, UserRoler.Admin);

            return Ok(new Reponse { status = "Success", message = "Người dùng đã được tạo thành công!" });
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach( var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Sucrekey"]));

                var token = new JwtSecurityToken(
                         issuer: _configuration["Jwt:ValidIssver"],
                         audience: _configuration["Jwt:ValidAudiunce"],
                         expires: DateTime.Now.AddHours(3),
                         claims: authClaims,
                         signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
            }
            return Unauthorized();

        }
    }
}

