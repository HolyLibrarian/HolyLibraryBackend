using System;
using System.Linq;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly HolyLibraryContext dbContext;

        public BorrowRecordsController(HolyLibraryContext holyLibraryContext)
        {
            dbContext = holyLibraryContext;
        }

        [HttpPost]
        public IActionResult CreateBorrowRecord(CreateBorrowRecordDto createBorrowRecordDto)
        {
            var user = dbContext.Users.Where(x => x.Id == createBorrowRecordDto.UserId).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            var collection = dbContext.Collections.Where(x => x.Id == createBorrowRecordDto.CollectionId).FirstOrDefault();
            if (collection == null)
            {
                return NotFound();
            }
            if (collection.IsBorrowed())
            {
                return Forbid();
            }
            var borrowRecord = new BorrowRecord
            {
                User = user,
                Collection = collection,
                CreateTime = DateTime.Now,
                ExpireTime = DateTime.Now.AddDays(createBorrowRecordDto.ExpireDays),
            };
            dbContext.Add(borrowRecord);
            user.BorrowCollection(collection);
            dbContext.Update(collection);
            dbContext.SaveChanges();
            return Created(borrowRecord.Id.ToString(), borrowRecord);
        }
    }
}
