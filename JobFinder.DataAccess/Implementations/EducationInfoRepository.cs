using JobFinder.Common.Exceptions;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;

namespace JobFinder.DataAccess.Implementations
{
    public class EducationInfoRepository : EfCoreRepository<EducationInfoEntity>, IEducationInfoRepository
    {
        public EducationInfoRepository(JobFinderDbContext context) : base(context)
        {

        }

        public async Task SetIncludeInAnonymousProfile(string cvId, int educationInfoId)
        {
            EducationInfoEntity educationInfo = await this.DbSet.FindAsync(educationInfoId);

            base.ValidateForExistence(educationInfo, "EducationInfo");

            if (educationInfo.CurriculumVitaeId != cvId)
            {
                throw new ActionableException("You can't modify foreign user cv details!");
            }

            educationInfo.IncludeInAnonymousProfile = true;

            this.DbSet.Update(educationInfo);
        }
    }
}
