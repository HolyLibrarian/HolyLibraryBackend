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

        [HttpGet]
        public IActionResult SearchReaders(string account = null, string name = null, string email = null, string phoneNumber = null)
        {
            var users = dbContext.Readers
                .Where(x => x.Account.Contains(account) || account == null)
                .Where(x => x.Name.Contains(name) || name == null)
                .Where(x => x.Email.Contains(email) || email == null)
                .Where(x => x.PhoneNumber.Contains(phoneNumber) || phoneNumber == null)
                .ToList();
            return Ok(users);
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

        [HttpPut("{userId}")]
        public object EditReader(int userId, EditReaderDto editReaderDto)
        {
            var user = dbContext.Readers.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            user.Account = editReaderDto.Account;
            user.Password = Argon2.Hash(editReaderDto.Password);
            user.Name = editReaderDto.Name;
            user.Email = editReaderDto.Email;
            user.PhoneNumber = editReaderDto.PhoneNumber;
            dbContext.Update(user);
            dbContext.SaveChanges();
            return Ok(user);
        }
    }
}
