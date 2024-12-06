namespace JobFinder.Data.SchemaDefinitions
{
    using JobFinder.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CompanyEntitySchemaDefinition : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            //one to many
            builder
                .HasMany(u => u.JobAds)
                .WithOne(j => j.Publisher)
                .HasForeignKey(j => j.PublisherId);

            //unique constrains
            builder
                .HasIndex(c => c.Bulstat)
                .IsUnique();

            builder
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
