using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using HolyLibraryBackend.Controllers;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HolyLibraryBackend.Test
{
    public class ManagersControllerTest
    {
        private ManagersController managersController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HolyLibraryContext>()
                .UseInMemoryDatabase("HolyLibrary")
                .Options;
            var dbContext = new HolyLibraryContext(options);
            managersController = new ManagersController(dbContext);
        }

        [Test]
        public void TestCreateManager()
        {
            var result = managersController.CreateManager(new CreateManagerDto
            {
                Password = "",
            });
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestSearchManagers()
        {
            managersController.CreateManager(new CreateManagerDto
            {
                Account = "123456",
                Password = "",
                Name = "123456",
                Email = "123456@ntut.edu.tw",
            });
            managersController.CreateManager(new CreateManagerDto
            {
                Account = "456789",
                Password = "",
                Name = "456789",
                Email = "456789@ntut.edu.tw",
            });

            var result = managersController.SearchManagers(account: "456");
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetManager()
        {
            managersController.CreateManager(new CreateManagerDto
            {
                Password = "",
            });
            var result = managersController.GetManager(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestEditManager()
        {
            managersController.CreateManager(new CreateManagerDto
            {
                Password = "",
            });
            var result = managersController.EditManager(1, new EditManagerDto
            {
                Account = "test",
                Password = "",
            });
            Assert.IsNotNull(result);
        }
    }
}
