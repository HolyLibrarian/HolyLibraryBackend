using System;
using System.Linq;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController
    {
        private readonly HolyLibraryContext holyLibraryContext;

        public ReservationController(HolyLibraryContext holyLibraryContext)
        {
            this.holyLibraryContext = holyLibraryContext;
        }

        [HttpPost]
        public Reservation Post(CreateReservationDto createReservationDto)
        {
            var reservation = new Reservation
            {
                User = holyLibraryContext.Users.Where(user => user.Id == createReservationDto.UserId).First(),
                Collection = holyLibraryContext.Collections.Where(collection => collection.Id == createReservationDto.CollectionId).First(),
                CreateTime = new DateTime(),
                ExpireTime = new DateTime(),
            };
            holyLibraryContext.Add(reservation);
            holyLibraryContext.SaveChanges();
            return reservation;
        }
    }
}
