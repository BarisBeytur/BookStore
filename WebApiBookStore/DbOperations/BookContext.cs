using Microsoft.EntityFrameworkCore;
using WebApiBookStore.Entities;

namespace WebApiBookStore.DbOperations
{
    public class BookContext : DbContext,IBookContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors  { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
