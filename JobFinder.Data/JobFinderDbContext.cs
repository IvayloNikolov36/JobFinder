namespace JobFinder.Data
{
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Subscriptions;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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

        public DbSet<CurriculumVitae> CVs { get; set; }

        public DbSet<DrivingCategoryType> DrivingCategoryTypes { get; set; }

        public DbSet<DrivingCategory> DrivingCategories { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<LanguageInfo> LanguagesInfo { get; set; }

        public DbSet<PersonalDetails> PersonalDetails { get; set; }

        public DbSet<WorkExperience> WorkExperiences { get; set; }

        public DbSet<CourseCertificate> CoursesCertificates { get; set; }

        public DbSet<CompanySubscription> CompanySubscriptions { get; set; }

        public DbSet<JobCategorySubscription> JobCategorySubscriptions { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

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
                .WithOne(j => j.Publisher)
                .HasForeignKey(j => j.PublisherId);

            //unique constrains
            builder.Entity<Company>()
                .HasIndex(c => c.Bulstat)
                .IsUnique();

            builder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<CompanySubscription>()
                .HasKey(x => new { x.UserId, x.CompanyId });

            builder.Entity<JobCategorySubscription>()
                .HasKey(x => new { x.UserId, x.JobCategoryId });

            base.OnModelCreating(builder);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
