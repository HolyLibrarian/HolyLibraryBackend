using System;
using System.Linq;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult SearchBorrowRecords(int userId, int collectionId)
        {
            var user = dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            var collection = dbContext.Collections.Where(x => x.Id == collectionId).FirstOrDefault();
            var borrowRecords = dbContext.BorrowRecords
                .Where(x => x.User == user || user == null)
                .Where(x => x.Collection == collection || collection == null)
                .Include(x => x.User)
                .Include(x => x.Collection)
                .ToList();
            return Ok(borrowRecords);
        }

        [HttpPost("{borrowRecordId}/isReturned")]
        public IActionResult MarkBorrowRecordAsReturned(int borrowRecordId)
        {
            var borrowRecord = dbContext.BorrowRecords
                .Where(x => x.Id == borrowRecordId)
                .Include(x => x.User)
                .Include(x => x.Collection)
                .FirstOrDefault();
            if (borrowRecord == null)
            {
                return NotFound();
            }
            if (borrowRecord.IsReturned)
            {
                return Forbid();
            }
            borrowRecord.User.ReturnCollection(borrowRecord.Collection);
            dbContext.Update(borrowRecord.Collection);
            borrowRecord.IsReturned = true;
            dbContext.Update(borrowRecord);
            dbContext.SaveChanges();
            return Created("", true);
        }
    }
}
