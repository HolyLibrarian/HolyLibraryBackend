using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using HolyLibraryBackend.Controllers;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;

namespace HolyLibraryBackend.Test
{
    public class ReservationsControllerTest
    {
        private ReservationsController reservationsController;
        private CollectionsController collectionsController;
        private ReadersController readersController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HolyLibraryContext>()
                .UseInMemoryDatabase("HolyLibrary")
                .Options;
            var dbContext = new HolyLibraryContext(options);
            reservationsController = new ReservationsController(dbContext);
            collectionsController = new CollectionsController(dbContext);
            readersController = new ReadersController(dbContext);
        }

        [Test]
        public void TestCreateReservation()
        {
            readersController.CreateReader(new CreateReaderDto
            {
                Password = ""
            });
            collectionsController.CreateCollection(new CreateCollecitonDto());
            var result = reservationsController.CreateReservation(new CreateReservationDto
            {
                UserId = 1,
                CollectionId = 1,
                ExpireDays = 7,
            });
            var reservation = (result as Microsoft.AspNetCore.Mvc.CreatedResult).Value as Reservation;
            Assert.AreEqual(1, reservation.User.Id);
            Assert.AreEqual(1, reservation.Collection.Id);
            Assert.AreEqual(7, (reservation.ExpireTime - reservation.CreateTime).Days);
        }
    }
}

