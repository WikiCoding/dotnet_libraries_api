using Library_Rest_API.Persistence.Datamodel;
using Microsoft.EntityFrameworkCore;

namespace Library_Rest_API.Persistence
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<LibraryDataModel> Libraries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryDataModel>().Property(e => e.Version).IsConcurrencyToken();
        }
    }
}
