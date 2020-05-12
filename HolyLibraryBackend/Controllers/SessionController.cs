using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly HolyLibraryContext dbContext;

        public SessionController(HolyLibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            var isExist = dbContext.Users.Where(user => user.Account == loginDto.Account).Count() > 0;
            if (!isExist)
            {
                return Unauthorized();
            }

            var user = dbContext.Users.Where(user => user.Account == loginDto.Account).First();
            if (!Argon2.Verify(user.Password, loginDto.Password))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim("sub", user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qcerhNduEegmMYnxYqy9wgXu"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("", "", claims, expires: DateTime.Now.AddMinutes(86400), signingCredentials: credentials);

            return Created("", new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Indentification = user.GetIdentification(),
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult CheckLogin()
        {
            ClaimsIdentity identity = (ClaimsIdentity)HttpContext.User.Identity;

            var userId = int.Parse(identity.Claims.First().Value);
            var isExist = dbContext.Users.Where(user => user.Id == userId).Count() > 0;
            if (!isExist)
            {
                return Unauthorized();
            }

            var user = dbContext.Users.Where(user => user.Id == userId).First();
            return Ok(new
            {
                Indentification = user.GetIdentification()
            });
        }
    }
}
