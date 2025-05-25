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
            .Where(c => courseCertificateInfoIds.Contains(c.Id))
            .ToArrayAsync();
        
        foreach (CourseCertificateEntity course in courses)
        {
            course.IncludeInAnonymousProfile = true;
        } 

        this.DbSet.UpdateRange(courses);
    }
}
