using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagersController : ControllerBase
    {
        private readonly HolyLibraryContext dbContext;

        public ManagersController(HolyLibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult CreateManager(CreateManagerDto createManagerDto)
        {
            var manager = new Manager
            {
                Account = createManagerDto.Account,
                Password = Argon2.Hash(createManagerDto.Password),
                Name = createManagerDto.Name,
                Email = createManagerDto.Email,
                PhoneNumber = createManagerDto.PhoneNumber,
            };
            dbContext.Add(manager);
            dbContext.SaveChanges();
            return Created(manager.Id.ToString(), manager);
        }

        [HttpGet]
        public IActionResult SearchManagers(string account = null, string name = null, string email = null, string phoneNumber = null)
        {
            var managers = dbContext.Managers
                .Where(x => x.Account.Contains(account) || account == null)
                .Where(x => x.Name.Contains(name) || name == null)
                .Where(x => x.Email.Contains(email) || email == null)
                .Where(x => x.PhoneNumber.Contains(phoneNumber) || phoneNumber == null)
                .ToList();
            return Ok(managers);
        }

        [HttpGet("{userId}")]
        public IActionResult GetManager(int userId)
        {
            var manager = dbContext.Managers.Where(x => x.Id == userId).FirstOrDefault();
            if (manager == null)
            {
                return NotFound();
            }
            return Ok(manager);
        }

        [HttpGet("{userId}")]
        public IActionResult EditManager(int userId, EditManagerDto editManagerDto)
        {
            var manager = dbContext.Managers.Where(x => x.Id == userId).FirstOrDefault();
            if (manager == null)
            {
                return NotFound();
            }
            manager.Account = editManagerDto.Account;
            manager.Password = Argon2.Hash(editManagerDto.Password);
            manager.Name = editManagerDto.Name;
            manager.Email = editManagerDto.Email;
            manager.PhoneNumber = editManagerDto.PhoneNumber;
            dbContext.Update(manager);
            dbContext.SaveChanges();
            return Ok(manager);
        }
    }
}
