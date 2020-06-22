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
            var readers = dbContext.Readers
                .Where(x => x.Account.Contains(account) || account == null)
                .Where(x => x.Name.Contains(name) || name == null)
                .Where(x => x.Email.Contains(email) || email == null)
                .Where(x => x.PhoneNumber.Contains(phoneNumber) || phoneNumber == null)
                .Where(x => x.Deleteflag.Equals(false))
                .ToList();
            return Ok(readers);
        }

        [HttpGet("{userId}")]
        public IActionResult GetReader(int userId)
        {
            var reader = dbContext.Readers.Where(x => x.Id == userId).FirstOrDefault();
            if (reader == null)
            {
                return NotFound();
            }
            return Ok(reader);
        }

        [HttpPut("{userId}")]
        public object EditReader(int userId, EditReaderDto editReaderDto)
        {
            var reader = dbContext.Readers.Where(x => x.Id == userId).FirstOrDefault();
            if (reader == null)
            {
                return NotFound();
            }
            reader.Account = editReaderDto.Account;
            reader.Password = Argon2.Hash(editReaderDto.Password);
            reader.Name = editReaderDto.Name;
            reader.Email = editReaderDto.Email;
            reader.PhoneNumber = editReaderDto.PhoneNumber;
            dbContext.Update(reader);
            dbContext.SaveChanges();
            return Ok(reader);
        }

        [HttpDelete("{userId}")]
        public object DeleteReader(int userId)
        {
            var reader = dbContext.Readers.Where(x => x.Id == userId).FirstOrDefault();
            if (reader == null)
            {
                return NotFound();
            }
            reader.Deleteflag = true;
            dbContext.Update(reader);
            dbContext.SaveChanges();
            return Ok(reader);
        }
    }
}
