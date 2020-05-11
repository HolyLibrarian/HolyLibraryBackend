using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

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
                Password = createManagerDto.Password,
                Name = createManagerDto.Name,
            };
            dbContext.Add(manager);
            dbContext.SaveChanges();
            return Created(manager.Id.ToString(), manager);
        }
    }
}
