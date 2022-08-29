using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChaosBackend.Data.Entities;
using ChaosBackend.DTOs.AccountDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChaosBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;


        public AccountsController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        //Creating optional User
        //public IActionResult CreateUser()
        //{
        //    var result1 = _userManager.CreateAsync(new AppUser { FullName = "Fazail Jabbarov", UserName = "fazailjabbarov" }, "Sumqayit123").Result;

        //    return Ok();
        //}


        /// <summary>
		/// This endpoint returns a token
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// 
		///     POST api/accounts/login
		///     {
		///         "username":"fazailjabbarov",
		///         "password": "Sumqayit123"
		///     }
		/// 
		/// </remarks>
		/// <param name="loginDto"></param>
		/// <returns></returns>

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            
            if (user == null)
            {
                return NotFound();
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return NotFound();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("FullName", user.FullName)
            };

            string keyStr = _configuration.GetSection("JWT:secret").Value;

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: creds,
                    expires: DateTime.Now.AddDays(3),
                    issuer: _configuration.GetSection("JWT:issuer").Value,
                    audience: _configuration.GetSection("JWT:audience").Value
                    );

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new {token = tokenStr});
        }
    }
}

