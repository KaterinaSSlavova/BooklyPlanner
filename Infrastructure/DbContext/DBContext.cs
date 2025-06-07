using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadingTask> ReadingTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
