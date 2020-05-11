using Microsoft.EntityFrameworkCore;

namespace HolyLibraryBackend.Models
{
    public class HolyLibraryContext : DbContext
    {
        public HolyLibraryContext(DbContextOptions<HolyLibraryContext> options)
         : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Collection> Collections { get; set; }
    }
}
