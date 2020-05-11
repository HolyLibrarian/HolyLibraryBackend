using Microsoft.EntityFrameworkCore;

namespace HolyLibraryBackend.Models
{
    public class HolyLibraryContext : DbContext
    {
        public HolyLibraryContext(DbContextOptions<HolyLibraryContext> options)
         : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
