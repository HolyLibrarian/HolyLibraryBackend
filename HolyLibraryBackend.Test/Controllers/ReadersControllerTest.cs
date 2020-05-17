using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using HolyLibraryBackend.Controllers;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

        [Test]
        public void TestGetReader()
        {
            var createReaderDto = new CreateReaderDto
            {
                Password = "",
            };
            readersController.CreateReader(createReaderDto);
            var result = readersController.GetReader(1);
            Assert.IsNotNull(result);
        }
    }
}
