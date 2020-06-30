using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using HolyLibraryBackend.Controllers;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;

namespace HolyLibraryBackend.Test
{
    public class BorrowRecordsControllerTest
    {
        private BorrowRecordsController borrowRecordsController;
        private CollectionsController collectionsController;
        private ReadersController readersController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HolyLibraryContext>()
                .UseInMemoryDatabase("HolyLibrary")
                .Options;
            var dbContext = new HolyLibraryContext(options);
            borrowRecordsController = new BorrowRecordsController(dbContext);
            collectionsController = new CollectionsController(dbContext);
            readersController = new ReadersController(dbContext);
        }

        [Test]
        public void TestCreateBorrowRecord()
        {
            readersController.CreateReader(new CreateReaderDto
            {
                Password = ""
            });
            collectionsController.CreateCollection(new CreateCollecitonDto());
            var result = borrowRecordsController.CreateBorrowRecord(new CreateBorrowRecordDto
            {
                UserId = 1,
                CollectionId = 1,
                ExpireDays = 7,
            });
            var borrowRecord = (result as Microsoft.AspNetCore.Mvc.CreatedResult).Value as BorrowRecord;
            Assert.AreEqual(1, borrowRecord.User.Id);
            Assert.AreEqual(1, borrowRecord.Collection.Id);
            Assert.AreEqual(7, (borrowRecord.ExpireTime - borrowRecord.CreateTime).Days);
        }

        [Test]
        public void TestSearchBorrowRecords()
        {
            readersController.CreateReader(new CreateReaderDto
            {
                Password = ""
            });
            collectionsController.CreateCollection(new CreateCollecitonDto());
            collectionsController.CreateCollection(new CreateCollecitonDto());
            borrowRecordsController.CreateBorrowRecord(new CreateBorrowRecordDto
            {
                UserId = 1,
                CollectionId = 1,
                ExpireDays = 7,
            });
            borrowRecordsController.CreateBorrowRecord(new CreateBorrowRecordDto
            {
                UserId = 1,
                CollectionId = 2,
                ExpireDays = 7,
            });
            var result = borrowRecordsController.SearchBorrowRecords(1, null);
            var borrowRecords = (result as Microsoft.AspNetCore.Mvc.OkObjectResult).Value;
            Assert.NotNull(borrowRecords);
        }
    }
}
