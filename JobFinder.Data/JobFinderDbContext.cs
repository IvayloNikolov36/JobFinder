namespace JobFinder.Data
{
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Data.SchemaDefinitions;
    using JobFinder.Data.Seeders;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
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

        public DbSet<LanguageInfo> LanguageInfos { get; set; }

        public DbSet<PersonalInfo> PersonalDetails { get; set; }

        public DbSet<WorkExperience> WorkExperiences { get; set; }

        public DbSet<CourseCertificate> CoursesCertificates { get; set; }

        public DbSet<CompanySubscription> CompanySubscriptions { get; set; }

        public DbSet<JobCategorySubscription> JobCategorySubscriptions { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Citizenship> Citizenships { get; set; }

        public DbSet<Gender> Gender { get; set; }

        public DbSet<BusinessSector> BusinessSectors { get; set; }

        public DbSet<LanguageType> LanguageTpes { get; set; }

        public DbSet<LanguageLevel> LanguageLevels { get; set; }

        // For VIEWS

        public DbSet<CompaniesSubscriptionsData> CompaniesSubscriptionsData { get; set; }

        public DbSet<JobCategoriesSubscriptionsData> JobCategoriesSubscriptionsData { get; set; }

        public DbSet<LatestCompanyJobAds> LatestCompanyJobAds { get; set; } // for function which returns table

        // DB Functions
        // [DbFunction("GetLatesJobAdsForSubscribers", Schema = "dbo")]

        public IQueryable<LatestCompanyJobAds>
            GetLatesJobAdsForSubscribers(int jobCategoryId, string location) =>
            Set<LatestCompanyJobAds>()
            .FromSqlInterpolated($"SELECT * FROM GetLatesJobAdsForSubscribers({jobCategoryId}, {location})");


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
            builder.ApplyConfiguration(new CompanyEntitySchemaDefinition());

            // one to one or one to zero connections

            builder.Entity<User>()
                .HasOne(u => u.Company)
                .WithOne(c => c.User)
                .HasForeignKey<Company>(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<CompanySubscription>()
                .HasKey(x => new { x.UserId, x.CompanyId });

            builder.Entity<JobCategorySubscription>()
                .HasAlternateKey(x => new { x.UserId, x.JobCategoryId, x.Location })
                .HasName("IX_JobCategorySubscription_TripleAK");

            // FOR Database VIEWS

            builder.Entity<CompaniesSubscriptionsData>()
                .HasNoKey()
                .ToView("CompanySubscriptionsData", "dbo");

            builder.Entity<JobCategoriesSubscriptionsData>()
                .HasNoKey()
                .ToView("SubscriprionsByJobCategoryAndLocation", "dbo");

            builder.Entity<LatestCompanyJobAds>()
                .HasNoKey()
                .ToView(null);

            this.SeedData(builder);

            base.OnModelCreating(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            CountriesSeeder.Seed(builder);
            CitizenshipsSeeder.Seed(builder);
            GenderSeeder.Seed(builder);
            BusinessSectorsSeeder.Seed(builder);
            JobCategorySeeder.Seed(builder);
            JobEngagementSeeder.Seed(builder);
            EducationLevelSeeder.Seed(builder);
            LanguageTypesSeeder.Seed(builder);
            LanguageLevelsSeeder.Seed(builder);
        }

        private void ApplyAuditInfoRules()
        {
            IEnumerable<EntityEntry> changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (EntityEntry entry in changedEntries)
            {
                IAuditInfo entity = (IAuditInfo)entry.Entity;

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
