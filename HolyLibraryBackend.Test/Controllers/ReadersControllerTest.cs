using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using HolyLibraryBackend.Controllers;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;

namespace HolyLibraryBackend.Test
{
    public class ReadersControllerTest
    {
        private ReadersController readersController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HolyLibraryContext>()
                .UseInMemoryDatabase("HolyLibrary")
                .Options;
            var dbContext = new HolyLibraryContext(options);
            readersController = new ReadersController(dbContext);
        }

        [Test]
        public void TestCreateReader()
        {
            var createReaderDto = new CreateReaderDto
            {
                Password = "",
            };
            var result = readersController.CreateReader(createReaderDto);
            Assert.IsNotNull(result);
        }
    }
}
