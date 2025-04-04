using JobFinder.DataAccess.Contracts;
using JobFinder.Transfer.Common;

namespace JobFinder.DataAccess.UnitOfWork
{
    public interface IEntityFrameworkUnitOfWork
    {
        ICompanySubscriptionsRepository CompanySubscriptionsRepository { get; }

        IJobAdSubscriptionsRepository JobAdSubscriptionsRepository { get; }

        Task SaveChanges();

        Task SaveChanges<DTO, IdType>(DTO dtoToPopulateId) where DTO : IUniquelyIdentified<IdType>;

        Task SaveChanges<DTO, IdType>(IEnumerable<DTO> dtosToPopulateId) where DTO : IUniquelyIdentified<IdType>;
    }
}
