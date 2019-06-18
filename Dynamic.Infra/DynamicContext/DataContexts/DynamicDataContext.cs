using Dynamic.Domain.DynamicContext.Entities;
using Dynamic.Infra.DynamicContext.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Dynamic.Infra.DynamicContext.DataContext
{
    public class DynamicDataContext : DbContext
    {
        public DynamicDataContext() { }

        public DynamicDataContext(DbContextOptions<DynamicDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}