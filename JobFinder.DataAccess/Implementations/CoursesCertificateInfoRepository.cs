using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CoursesCertificateInfoRepository
    : EfCoreRepository<CourseCertificateEntity>, ICoursesCertificateInfoRepository
{
    public CoursesCertificateInfoRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> courseCertificateInfoIds)
    {
        CourseCertificateEntity[] courses = await this.DbSet
            .Where(c => c.CurriculumVitaeId == cvId)
            .ToArrayAsync();
        
        foreach (CourseCertificateEntity course in courses)
        {
            course.IncludeInAnonymousProfile = courseCertificateInfoIds.Contains(course.Id);
        } 

        this.DbSet.UpdateRange(courses);
    }
}
