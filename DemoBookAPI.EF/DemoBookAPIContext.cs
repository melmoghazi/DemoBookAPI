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
        public DbSet<CategoryBooks> CategoryBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Book>()
            //            .HasMany(s => s.Categories) // Book can enroll in many Categories
            //            .WithMany(c => c.Books) // Category can have many Books
            //            .UsingEntity(j => j.ToTable("BookCategories"));  //Explicitly set the join table name

            modelBuilder.Entity<Book>()
                        .HasMany(e => e.Categories)
                        .WithMany(e => e.Books)
                        .UsingEntity<CategoryBooks>();
        }
    }
}
