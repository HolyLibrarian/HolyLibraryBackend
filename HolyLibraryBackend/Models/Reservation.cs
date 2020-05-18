using System;

namespace HolyLibraryBackend.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Collection Collection { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsFulfilled { get; set; }
    }
}
