using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly HolyLibraryContext dbContext;

        public ReservationsController(HolyLibraryContext holyLibraryContext)
        {
            dbContext = holyLibraryContext;
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateReservationDto createReservationDto)
        {
            var user = dbContext.Users.Where(x => x.Id == createReservationDto.UserId).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            var collection = dbContext.Collections.Where(x => x.Id == createReservationDto.CollectionId).FirstOrDefault();
            if (collection == null)
            {
                return NotFound();
            }
            if (!collection.IsBorrowed())
            {
                return Forbid();
            }
            var userReservation = dbContext.Reservations
                .Where(x => x.User == user)
                .Where(x => x.Collection == collection )
                .Where(x => x.IsCanceled.Equals(false))
                .Include(x => x.User)
                .Include(x => x.Collection)
                .SingleOrDefault();

            if(userReservation != null)
            {
                return NotFound();
            }

            var lastBorrowRecord = dbContext.BorrowRecords
                .Where(x => x.User == user || user == null)
                .Where(x => x.Collection == collection || collection == null)
                .Include(x => x.User)
                .Include(x => x.Collection)
                .OrderBy(x => x.ExpireTime)
                .SingleOrDefault();

            var reservation = new Reservation
            {
                User = user,
                Collection = collection,
                CreateTime = DateTime.Now,
                ExpireTime = lastBorrowRecord.ExpireTime.AddDays(createReservationDto.ExpireDays),
            };
            dbContext.Add(reservation);
            dbContext.SaveChanges();
            return Created(reservation.Id.ToString(), reservation);
        }

        [HttpGet]
        public IActionResult SearchReservation(int userId, int collectionId)
        {
            var user = dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            var reservations = dbContext.Reservations
                .Where(x => x.User == user || user == null)
                .Where(x => x.IsCanceled.Equals(false))
                .Include(x => x.User)
                .Include(x => x.Collection)
                .ToList();
            return Ok(reservations);
        }

        [HttpPost("{reservationId}/isCanceled")]
        public IActionResult MarkReservationAsCanceled(int reservationId)
        {
            var reservation = dbContext.Reservations
                .Where(x => x.Id == reservationId)
                .Include(x => x.User)
                .Include(x => x.Collection)
                .FirstOrDefault();
            if (reservation == null)
            {
                return NotFound();
            }
            if (reservation.IsCanceled)
            {
                return Forbid();
            }
            if (reservation.IsFulfilled)
            {
                return Forbid();
            }
            reservation.IsCanceled = true;
            dbContext.Update(reservation);
            dbContext.SaveChanges();
            return Created("", true);
        }

        [HttpPost("{reservationId}/isFulfilled")]
        public IActionResult MarkReservationAsFulfilled(int reservationId)
        {
            var reservation = dbContext.Reservations
                .Where(x => x.Id == reservationId)
                .Include(x => x.User)
                .Include(x => x.Collection)
                .FirstOrDefault();
            if (reservation == null)
            {
                return NotFound();
            }
            if (reservation.IsCanceled)
            {
                return Forbid();
            }
            if (reservation.IsFulfilled)
            {
                return Forbid();
            }
            reservation.User.BorrowCollection(reservation.Collection);
            dbContext.Update(reservation.Collection);
            reservation.IsFulfilled = true;
            dbContext.Update(reservation);
            dbContext.SaveChanges();
            return Created("", true);
        }
    }
}
