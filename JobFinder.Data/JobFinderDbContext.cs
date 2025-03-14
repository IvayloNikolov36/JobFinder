﻿namespace JobFinder.Data
{
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Cv;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Nomenclature;
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

    public class JobFinderDbContext : IdentityDbContext<UserEntity>
    {
        public JobFinderDbContext(DbContextOptions<JobFinderDbContext> options) : base(options)
        {
        }

        public DbSet<CompanyEntity> Companies { get; set; }

        public DbSet<JobAdvertisementEntity> JobAdvertisements { get; set; }

        public DbSet<CurriculumVitaeEntity> CurriculumVitaes { get; set; }

        public DbSet<EducationInfoEntity> EducationInfos { get; set; }

        public DbSet<SkillsInfoEntity> SkillsInfos { get; set; }

        public DbSet<DrivingCategoryEntity> DrivingCategoryTypes { get; set; }

        public DbSet<SkillsInfoDrivingCategoryEntity> SkillsInfosDrivingCategories { get; set; }

        public DbSet<LanguageInfoEntity> LanguageInfos { get; set; }

        public DbSet<PersonalInfoEntity> PersonalInfos { get; set; }

        public DbSet<WorkExperienceInfoEntity> WorkExperienceInfos { get; set; }

        public DbSet<CourseCertificateEntity> CourseCertificatesInfos { get; set; }

        public DbSet<CompanySubscriptionEntity> CompanySubscriptions { get; set; }

        public DbSet<JobsSubscriptionEntity> JobsSubscriptions { get; set; }


        // Nomenclature Entities

        public DbSet<CountryEntity> Countries { get; set; }

        public DbSet<CitizenshipEntity> Citizenships { get; set; }

        public DbSet<GenderEntity> Gender { get; set; }

        public DbSet<BusinessSectorEntity> BusinessSectors { get; set; }

        public DbSet<LanguageTypeEntity> LanguageTpes { get; set; }

        public DbSet<LanguageLevelEntity> LanguageLevels { get; set; }

        public DbSet<JobEngagementEntity> JobEngagements { get; set; }

        public DbSet<JobCategoryEntity> JobCategories { get; set; }

        public DbSet<JobAdApplicationEntity> JobAdsApplications { get; set; }

        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<CurrencyEntity> Currencies { get; set; }

        public DbSet<ReccuringTypeEntity> ReccuringTypes { get; set; }

        // For VIEWS

        public DbSet<JobAdsSubscriptionsDbVewData> JobAdsSubscriptionsData { get; set; }


        // For table value function

        public DbSet<LatestJobAdsDbFunctionResult> LatestJobAds { get; set; }

        public DbSet<CompaniesSubscriptionsFunctionResult> CompaniesSubscriptions { get; set; }


        // DB Functions

        [DbFunction("udf_GetLatesJobAdsForSubscribers", Schema = "dbo")]
        public IQueryable<LatestJobAdsDbFunctionResult> GetLatesJobAdsForSubscribersDbFunction(
                int reccuringTypeId,
                int? jobCategoryId,
                int? jobEngagementId,
                int? locationId,
                string searchTerm,
                bool intership,
                bool specifiedSalary) =>
            Set<LatestJobAdsDbFunctionResult>()
                .FromSqlInterpolated(@$"SELECT * FROM [udf_GetLatesJobAdsForSubscribers] (
                    {reccuringTypeId},
                    {jobCategoryId},
                    {jobEngagementId},
                    {locationId},
                    {searchTerm},
                    {intership},
                    {specifiedSalary})");


        [DbFunction("udf_GetLatestJobAdsForCompanySubscriptions", Schema = "dbo")]
        public IQueryable<CompaniesSubscriptionsFunctionResult> GetLatesJobAdsForCompanySubscriptionsDbFunction(int recurringTypeId) =>
            Set<CompaniesSubscriptionsFunctionResult>()
            .FromSqlInterpolated(@$"SELECT * FROM [udf_GetLatestJobAdsForCompanySubscriptions] ({recurringTypeId})");


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

            builder.Entity<UserEntity>()
                .HasOne(u => u.Company)
                .WithOne(c => c.User)
                .HasForeignKey<CompanyEntity>(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<CompanySubscriptionEntity>()
                .HasKey(x => new { x.UserId, x.CompanyId });

            builder.Entity<JobAdApplicationEntity>()
                .HasIndex(j => j.ApplicantId);

            builder.Entity<JobAdApplicationEntity>()
                .HasIndex(j => j.JobAdId);


            // VIEWS

            builder.Entity<JobAdsSubscriptionsDbVewData>()
                .HasNoKey()
                .ToView("view_JobSubscriptions", "dbo");

            // FUNCTIONS

            builder.Entity<LatestJobAdsDbFunctionResult>()
                .HasNoKey()
                .ToView(null);

            builder.Entity<CompaniesSubscriptionsFunctionResult>()
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
            DrivingCategorySeeder.Seed(builder);
            CitiesSeeder.Seed(builder);
            CurrenciesSeeder.Seed(builder);
            ReccuringTypesSeeder.Seed(builder);
        }

        private void ApplyAuditInfoRules()
        {
            IEnumerable<EntityEntry> changedEntries = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditInfo
                    && (e.State == EntityState.Added || e.State == EntityState.Modified));

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
