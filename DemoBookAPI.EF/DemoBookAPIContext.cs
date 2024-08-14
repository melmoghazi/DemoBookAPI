using DemoBookAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoBookAPI.EF
{
    public class DemoBookAPIContext : DbContext
    {
        public DemoBookAPIContext(DbContextOptions<DemoBookAPIContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
        }
    }
}
