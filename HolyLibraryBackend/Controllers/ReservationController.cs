using System;
using System.Linq;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
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
                User = holyLibraryContext.Users.Where(x => x.Id == createReservationDto.UserId).FirstOrDefault(),
                Collection = holyLibraryContext.Collections.Where(x => x.Id == createReservationDto.CollectionId).FirstOrDefault(),
                CreateTime = new DateTime(),
                ExpireTime = new DateTime(),
            };
            holyLibraryContext.Add(reservation);
            holyLibraryContext.SaveChanges();
            return reservation;
        }
    }
}
