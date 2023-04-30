using Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DevEventDbContext : DbContext
    {

        public DevEventDbContext(DbContextOptions<DevEventDbContext> options) : base(options){ }
        public DbSet<DevEvent> Events { get; set; }
        public DbSet<DevEventSpeaker> Speakers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DevEvent>(e =>
            {
                e.HasKey(de => de.id);

                e.Property(de => de.Title).IsRequired(false);

                e.Property(de => de.Description)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                e.Property(de => de.StartDate)
                    .HasColumnName("start_date");

                e.Property(de => de.EndDate)
                    .HasColumnName("end_date");

                e.HasMany(de => de.Speakers)
                .WithOne()
                .HasForeignKey(de => de.Id);
            });

            builder.Entity<DevEventSpeaker>(e =>
            {
                e.HasKey(de => de.Id);
            });
        }
    }
}