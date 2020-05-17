using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using HolyLibraryBackend.Controllers;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HolyLibraryBackend.Test
{
    public class CollectionsControllerTest
    {
        private CollectionsController collectionsController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HolyLibraryContext>()
                .UseInMemoryDatabase("HolyLibrary")
                .Options;
            var dbContext = new HolyLibraryContext(options);
            collectionsController = new CollectionsController(dbContext);
        }

        [Test]
        public void TestCreateCollection()
        {
            var result = collectionsController.CreateCollection(new CreateCollecitonDto());
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestSearchCollections()
        {
            collectionsController.CreateCollection(new CreateCollecitonDto
            {
                Name = "123456",
            });
            collectionsController.CreateCollection(new CreateCollecitonDto
            {
                Name = "456789",
            });
            var result = collectionsController.SearchCollections(name: "456");
            Assert.IsNotNull(result);
        }
    }
}
