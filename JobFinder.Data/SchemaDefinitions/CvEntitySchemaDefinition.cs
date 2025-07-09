using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.Data.Models.Cv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFinder.Data.SchemaDefinitions;

public class CvEntitySchemaDefinition : IEntityTypeConfiguration<CurriculumVitaeEntity>
{
    public void Configure(EntityTypeBuilder<CurriculumVitaeEntity> builder)
    {
        builder
            .HasOne(cv => cv.PersonalInfo)
            .WithOne(pi => pi.Cv)
            .HasForeignKey<PersonalInfoEntity>(pi => pi.CvId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(cv => cv.Educations)
            .WithOne(e => e.Cv)
            .HasForeignKey(e => e.CvId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(cv => cv.WorkExperiences)
            .WithOne(we => we.Cv)
            .HasForeignKey(we => we.CvId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(cv => cv.LanguagesInfo)
            .WithOne(li => li.Cv)
            .HasForeignKey(li => li.CvId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(cv => cv.CourseCertificates)
            .WithOne(cs => cs.Cv)
            .HasForeignKey(cs => cs.CvId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(cv => cv.Skills)
            .WithOne(s => s.Cv)
            .HasForeignKey<SkillsInfoEntity>(s => s.CvId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(cv => cv.AnonymousProfile)
            .WithOne(ap => ap.Cv)
            .HasForeignKey<AnonymousProfileEntity>(ap => ap.CvId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(cv => cv.CvPreviewRequests)
            .WithOne(cr => cr.Cv)
            .HasForeignKey(cr => cr.CvId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
