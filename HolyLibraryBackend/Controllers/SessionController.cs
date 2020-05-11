using System.Linq;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;

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

            return Created("", new
            {
                Jwt = "878787",
            });
        }
    }
}
