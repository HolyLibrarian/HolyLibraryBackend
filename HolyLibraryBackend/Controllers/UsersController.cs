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
        public User CreateReader(CreateReaderDto createReaderDto)
        {
            var user = new Reader
            {
                Account = createReaderDto.Account,
                Password = createReaderDto.Password,
                Name = createReaderDto.Name,
                Email = createReaderDto.Email,
                PhoneNumber = createReaderDto.PhoneNumber,
                MaxBorrowNumber = createReaderDto.MaxBorrowNumber,
            };
            holyLibraryContext.Add(user);
            holyLibraryContext.SaveChanges();
            return user;
        }

        [HttpPost("manager")]
        public User CreateManager(CreateManagerDto createManagerDto)
        {
            var user = new Manager
            {
                Account = createManagerDto.Account,
                Password = createManagerDto.Password,
                Name = createManagerDto.Name,
            };
            holyLibraryContext.Add(user);
            holyLibraryContext.SaveChanges();
            return user;
        }
    }
}
