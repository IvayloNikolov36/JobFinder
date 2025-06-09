using JobFinder.Data;
using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;

namespace JobFinder.DataAccess.Implementations;

public class AnonymousProfileAppearanceRepository :
    EfCoreRepository<AnonymousProfileAppearanceEntity>,
    IAnonymousProfileAppearanceRepository
{
    public AnonymousProfileAppearanceRepository(JobFinderDbContext context) : base(context)
    {
    }
}
