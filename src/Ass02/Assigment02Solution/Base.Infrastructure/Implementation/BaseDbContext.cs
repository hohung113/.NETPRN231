using Microsoft.EntityFrameworkCore;
namespace Base.Infrastructure.Implementation
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() { }

        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        //public DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        //{
        //    return base.Set<TEntity>();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<T>()
            //    .HasQueryFilter(e => !e.IsDeleted); 

            //modelBuilder.Entity<T>()
            //    .HasKey(a => new { a.Key1, a.Key2 });
        }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
