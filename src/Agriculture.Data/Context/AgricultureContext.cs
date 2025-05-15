using Microsoft.EntityFrameworkCore;
using Agriculture.Data.Models;

namespace Agriculture.Data.Context
{
    public class AgricultureContext : DbContext
    {
        public AgricultureContext(DbContextOptions<AgricultureContext> options)
            : base(options)
        {
        }

        public DbSet<Sensor> Sensors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Value).HasPrecision(10, 2);
                entity.Property(e => e.LastReading).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();
            });
        }
    }
}