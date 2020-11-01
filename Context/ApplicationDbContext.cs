using System;
using System.Threading;
using System.Threading.Tasks;
using BlogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Status = true;
                        entry.Entity.Created = new DateTime().Date;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modified = new DateTime().Date;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                    
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(entity => {
                entity.ToTable("Category");
            });
        }
    }
}