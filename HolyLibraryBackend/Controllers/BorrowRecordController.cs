using System;
using System.Linq;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowRecordController : ControllerBase
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
                User = holyLibraryContext.Users.Where(x => x.Id == createBorrowRecordDto.UserId).FirstOrDefault(),
                Collection = holyLibraryContext.Collections.Where(x => x.Id == createBorrowRecordDto.CollectionId).FirstOrDefault(),
                CreateTime = new DateTime(),
                ExpireTime = new DateTime(),
            };
            holyLibraryContext.Add(borrowRecord);
            holyLibraryContext.SaveChanges();
            return borrowRecord;
        }
    }
}
