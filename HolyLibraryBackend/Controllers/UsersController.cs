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

        [HttpPost]
        public User CreateUser(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Account = createUserDto.Account,
                Password = createUserDto.Pasword,
                Name = createUserDto.Name,
                Email = createUserDto.Email,
            };
            holyLibraryContext.Add(user);
            holyLibraryContext.SaveChanges();
            return user;
        }
    }
}
