using JobFinder.Data.Models;
using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.Data.Models.Cv;
using JobFinder.Data.Models.Nomenclature;
using JobFinder.Data.Models.Subscriptions;
using JobFinder.Data.Models.ViewsModels;
using JobFinder.Data.SchemaDefinitions;
using JobFinder.Data.Seeders;
using JobFinder.Transfer.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JobFinder.Data
{
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

        public DbSet<AnonymousProfileEntity> AnonymousProfiles { get; set; }

        public DbSet<AnonymousProfileAppearanceEntity> AnonymousProfileAppearances { get; set; }

        public DbSet<AnonymousProfileAppearanceITAreaEntity> AnonymousProfileAppearancesITAreas { get; set; }

        public DbSet<AnonymousProfileAppearanceSoftSkillEntity> AnonymousProfileAppearancesSoftSkills { get; set; }

        public DbSet<AnonymousProfileAppearanceTechStackEntity> AnonymousProfileAppearancesTechStacks { get; set; }

        public DbSet<AnonymousProfileAppearanceJobEngagementEntity> AnonymousProfileAppearancesJobEngagements { get; set; }

        public DbSet<JobAdvertisementSoftSkillEntity> JobAdvertisementsSoftSkills { get; set; }

        public DbSet<JobAdvertisementITAreaEntity> JobAdvertisementsITAreas { get; set; }

        public DbSet<JobAdvertisementTechStackEntity> JobAdvertisementsTechStacks { get; set; }

        public DbSet<AnonymousProfileAppearanceWorkplaceTypeEntity> AnonymousProfileAppearancesWorkplaceTypes { get; set; }

        public DbSet<AnonymousProfileAppearanceCityEntity> AnonymousProfileAppearancesCities { get; set; }


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

        public DbSet<RecurringTypeEntity> RecurringTypes { get; set; }

        public DbSet<ITAreaEntity> ItAreas { get; set; }

        public DbSet<SoftSKillEntity> SoftSkills { get; set; }

        public DbSet<TechStackEntity> TechStacks { get; set; }

        public DbSet<WorkplaceTypeEntity> WorkplaceTypes { get; set; }


        // For VIEWS

        public DbSet<CompanyJobAdsForSubscribersViewData> CompanyJobAdsForSubscribersView { get; set; }


        // For table value function

        public DbSet<LatestJobAdsDbFunctionResult> LatestJobAds { get; set; }

        public DbSet<JobAdsSubscriptionsDbFunctionResult> JobAdsSubscriptionsData { get; set; }


        // DB Functions

        [DbFunction("udf_GetJobSubscriptions", Schema = "dbo")]
        public IQueryable<JobAdsSubscriptionsDbFunctionResult> GetJobAdsSubscriptionsDbFunction(int recurringTypeId) =>
            Set<JobAdsSubscriptionsDbFunctionResult>()
            .FromSqlInterpolated(@$"SELECT * FROM [udf_GetJobSubscriptions] ({recurringTypeId})");


        [DbFunction("udf_GetLatesJobAdsForSubscribers", Schema = "dbo")]
        public IQueryable<LatestJobAdsDbFunctionResult> GetLatesJobAdsForSubscribersDbFunction(
                int recurringTypeId,
                int? jobCategoryId,
                int? jobEngagementId,
                int? locationId,
                string searchTerm,
                bool intership,
                bool specifiedSalary) =>
            Set<LatestJobAdsDbFunctionResult>()
            .FromSqlInterpolated(@$"SELECT * FROM [udf_GetLatesJobAdsForSubscribers] (
                {recurringTypeId},
                {jobCategoryId},
                {jobEngagementId},
                {locationId},
                {searchTerm},
                {intership},
                {specifiedSalary})"
            );


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

            builder.Entity<JobAdvertisementEntity>()
                .Property(j => j.IsActive)
                .HasDefaultValue(true);

            builder.Entity<CompanySubscriptionEntity>()
                .HasKey(key => new { key.UserId, key.CompanyId });

            builder.Entity<JobAdApplicationEntity>()
                .HasIndex(j => j.ApplicantId);

            builder.Entity<JobAdApplicationEntity>()
                .HasIndex(j => j.JobAdId);

            builder.Entity<JobAdvertisementEntity>()
                .ToTable(t => t.HasCheckConstraint("CHK_JobAdvertisements_MinSalary_MaxSalary_Currency",
                     @"(CurrencyId IS NOT NULL AND MinSalary IS NOT NULL AND MaxSalary IS NOT NULL AND MinSalary <= MaxSalary)
                        OR (CurrencyId IS NULL AND MinSalary IS NULL AND MaxSalary IS NULL)
                     ")
                );

            builder.Entity<AnonymousProfileAppearanceEntity>()
                .HasOne(x => x.AnonymousProfile)
                .WithOne(ap => ap.Appearance)
                .IsRequired(true);

            builder.Entity<AnonymousProfileAppearanceJobEngagementEntity>()
                .HasKey(key => new { key.AnonymousProfileAppearanceId, key.JobEngagementId });

            builder.Entity<AnonymousProfileAppearanceSoftSkillEntity>()
                .HasKey(key => new { key.AnonymousProfileAppearanceId, key.SoftSkillId });

            builder.Entity<AnonymousProfileAppearanceITAreaEntity>()
                .HasKey(key => new { key.AnonymousProfileAppearanceId, key.ITAreaId });

            builder.Entity<AnonymousProfileAppearanceTechStackEntity>()
                .HasKey(key => new { key.AnonymousProfileAppearanceId, key.TechStackId });

            builder.Entity<AnonymousProfileAppearanceJobEngagementEntity>()
                .HasKey(key => new { key.AnonymousProfileAppearanceId, key.JobEngagementId });

            builder.Entity<JobAdvertisementSoftSkillEntity>()
                .HasKey(key => new { key.JobAdvertisementId, key.SoftSkillId });

            builder.Entity<JobAdvertisementTechStackEntity>()
                .HasKey(key => new { key.JobAdvertisementId, key.TechStackId });

            builder.Entity<JobAdvertisementITAreaEntity>()
                .HasKey(key => new { key.JobAdvertisementId, key.ITAreaId });

            // VIEWS

            builder.Entity<CompanyJobAdsForSubscribersViewData>()
                .HasNoKey()
                .ToView("view_latestCompanyJobsForSubscribers", "dbo");


            // FUNCTIONS

            builder.Entity<JobAdsSubscriptionsDbFunctionResult>()
                .HasNoKey()
                .ToView(null);

            builder.Entity<LatestJobAdsDbFunctionResult>()
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
            RecurringTypesSeeder.Seed(builder);
            ITAreasSeeder.Seed(builder);
            SoftSkillsSeeder.Seed(builder);
            TechStacksSeeder.Seed(builder);
            WorkplaceTypesSeeder.Seed(builder);
        }

        private void ApplyAuditInfoRules()
        {
            IEnumerable<EntityEntry> changedEntries = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAudit
                    && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (EntityEntry entry in changedEntries)
            {
                IAudit entity = (IAudit)entry.Entity;

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
