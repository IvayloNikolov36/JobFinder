using JobFinder.Common.Exceptions;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;

namespace JobFinder.DataAccess.Implementations;

public class CoursesCertificateInfoRepository
    : EfCoreRepository<CourseCertificateEntity>, ICoursesCertificateInfoRepository
{
    public CoursesCertificateInfoRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task SetIncludeInAnonymousProfile(string cvId, int courseCertificateInfoId)
    {
        CourseCertificateEntity course = await this.DbSet.FindAsync(courseCertificateInfoId);

        base.ValidateForExistence(course, "CourseCertificateInfo");

        if (course.CurriculumVitaeId != cvId)
        {
            throw new ActionableException("You can't modify foreign user cv details!");
        }

        course.IncludeInAnonymousProfile = true;

        this.DbSet.Update(course);
    }
}
