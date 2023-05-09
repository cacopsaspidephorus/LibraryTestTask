using LibraryTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryTestTask.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<History> Histories { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Reader>().ToTable("Reader");
            modelBuilder.Entity<History>().ToTable("History");
        }
    }
}
