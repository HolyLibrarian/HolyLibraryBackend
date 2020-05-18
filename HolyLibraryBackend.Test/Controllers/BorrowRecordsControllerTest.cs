using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using HolyLibraryBackend.Controllers;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
            Assert.IsNotNull(result);
        }
    }
}
