using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
        public IActionResult CreateReader(CreateReaderDto createReaderDto)
        {
            var reader = new Reader
            {
                Account = createReaderDto.Account,
                Password = Argon2.Hash(createReaderDto.Password),
                Name = createReaderDto.Name,
                Email = createReaderDto.Email,
                PhoneNumber = createReaderDto.PhoneNumber,
                MaxBorrowNumber = createReaderDto.MaxBorrowNumber,
            };
            dbContext.Add(reader);
            dbContext.SaveChanges();
            return Created(reader.Id.ToString(), reader);
        }

        [HttpGet("{userId}")]
        public IActionResult GetReader(int userId)
        {
            var user = dbContext.Readers.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
