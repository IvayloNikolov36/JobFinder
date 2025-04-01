using JobFinder.DataAccess.Contracts;

namespace JobFinder.DataAccess.UnitOfWork
{
    public interface IEntityFrameworkUnitOfWork
    {
        ICompanySubscriptionsRepository CompanySubscriptionsRepository { get; }

        IJobAdSubscriptionsRepository JobAdSubscriptionsRepository { get; }

        Task SaveChanges();
    }
}
