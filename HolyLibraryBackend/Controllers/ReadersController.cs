using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadersController : ControllerBase
    {
        private readonly HolyLibraryContext dbContext;

        public ReadersController(HolyLibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public Reader CreateReader(CreateReaderDto createReaderDto)
        {
            var reader = new Reader
            {
                Account = createReaderDto.Account,
                Password = createReaderDto.Password,
                Name = createReaderDto.Name,
                Email = createReaderDto.Email,
                PhoneNumber = createReaderDto.PhoneNumber,
                MaxBorrowNumber = createReaderDto.MaxBorrowNumber,
            };
            dbContext.Add(reader);
            dbContext.SaveChanges();
            return reader;
        }
    }
}
