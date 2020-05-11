using System.Linq;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HolyLibraryContext holyLibraryContext;

        public UsersController(HolyLibraryContext holyLibraryContext)
        {
            this.holyLibraryContext = holyLibraryContext;
        }

        [HttpPost("reader")]
        public User CreateReader(CreateUserDto createUserDto)
        {
            var user = new Reader
            {
                Account = createUserDto.Account,
                Password = createUserDto.Password,
                Name = createUserDto.Name,
                Email = createUserDto.Email,
            };
            holyLibraryContext.Add(user);
            holyLibraryContext.SaveChanges();
            return user;
        }

        [HttpPost("manager")]
        public User CreateManager(CreateUserDto createUserDto)
        {
            var user = new Manager
            {
                Account = createUserDto.Account,
                Password = createUserDto.Password,
                Name = createUserDto.Name,
                Email = createUserDto.Email,
            };
            holyLibraryContext.Add(user);
            holyLibraryContext.SaveChanges();
            return user;
        }
    }
}
