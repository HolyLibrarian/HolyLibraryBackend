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
        public BorrowRecord Post(CreateBorrowRecordDto createBorrowRecordDto)
        {
            var borrowRecord = new BorrowRecord
            {
                User = dbContext.Users.Where(x => x.Id == createBorrowRecordDto.UserId).FirstOrDefault(),
                Collection = dbContext.Collections.Where(x => x.Id == createBorrowRecordDto.CollectionId).FirstOrDefault(),
                CreateTime = new DateTime(),
                ExpireTime = new DateTime(),
            };
            dbContext.Add(borrowRecord);
            dbContext.SaveChanges();
            return borrowRecord;
        }
    }
}
