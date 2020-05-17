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
            var result = readersController.CreateReader(new CreateReaderDto
            {
                Password = "",
            });
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestSearchReaders()
        {
            readersController.CreateReader(new CreateReaderDto
            {
                Account = "123456",
                Password = "",
                Name = "123456",
                Email = "123456@ntut.edu.tw",
            });
            readersController.CreateReader(new CreateReaderDto
            {
                Account = "456789",
                Password = "",
                Name = "456789",
                Email = "456789@ntut.edu.tw",
            });

            var result = readersController.SearchReaders(account: "456");
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetReader()
        {
            readersController.CreateReader(new CreateReaderDto
            {
                Password = "",
            });
            var result = readersController.GetReader(1);
            Assert.IsNotNull(result);
        }
    }
}
