using DataAccess.Entities;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbContextConfig
{
    public class BookDbContext : DbContext
    {
        public BookDbContext() { }
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfiguration configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("eStore"));
            }
        }
    }
}
