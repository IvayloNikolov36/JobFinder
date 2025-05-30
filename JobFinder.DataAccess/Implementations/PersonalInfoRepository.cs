using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;

namespace JobFinder.DataAccess.Implementations;

public class PersonalInfoRepository : EfCoreRepository<PersonalInfoEntity>, IPersonalInfoRepository
{
    public PersonalInfoRepository(JobFinderDbContext context) : base(context)
    {

    }

    public void Delete(string cvId)
    {
        base.DeleteWhere(x => x.CurriculumVitaeId == cvId);
    }
}
