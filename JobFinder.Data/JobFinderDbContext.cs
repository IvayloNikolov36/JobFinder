using JobFinder.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data
{
    public class JobFinderDbContext : IdentityDbContext<User>
    {
        public JobFinderDbContext(DbContextOptions<JobFinderDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<JobAd> JobAds { get; set; }

        public DbSet<JobEngagement> JobEngagements { get; set; }

        public DbSet<JobCategory> JobCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //one to one or zero
            builder.Entity<User>()
                .HasOne(u => u.Company)
                .WithOne(c => c.User)
                .HasForeignKey<Company>(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(builder);

            //one to many
            builder.Entity<User>()
                .HasMany(u => u.JobAds)
                .WithOne(ro => ro.Publisher)
                .HasForeignKey(ro => ro.PublisherId);
        }
    }
}
