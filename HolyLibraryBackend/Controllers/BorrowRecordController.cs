using System;
using System.Linq;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowRecordController
    {
        private readonly HolyLibraryContext holyLibraryContext;

        public BorrowRecordController(HolyLibraryContext holyLibraryContext)
        {
            this.holyLibraryContext = holyLibraryContext;
        }

        [HttpPost]
        public BorrowRecord Post(CreateBorrowRecordDto createBorrowRecordDto)
        {
            var borrowRecord = new BorrowRecord
            {
                User = holyLibraryContext.Users.Where(user => user.Id == createBorrowRecordDto.UserId).First(),
                Collection = holyLibraryContext.Collections.Where(collection => collection.Id == createBorrowRecordDto.CollectionId).First(),
                CreateTime = new DateTime(),
                ExpireTime = new DateTime(),
            };
            holyLibraryContext.Add(borrowRecord);
            holyLibraryContext.SaveChanges();
            return borrowRecord;
        }
    }
}
