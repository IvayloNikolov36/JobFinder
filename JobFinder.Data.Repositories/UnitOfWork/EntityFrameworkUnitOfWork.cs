using AutoMapper;
using JobFinder.Data;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Implementations;

namespace JobFinder.DataAccess.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : IEntityFrameworkUnitOfWork
    {
        private readonly JobFinderDbContext dbContext;
        private readonly IMapper mapper;

        private ICompanySubscriptionsRepository companySubscriptionsRepository;
        private IJobAdSubscriptionsRepository jobAdSubscriptionsRepository;

        public EntityFrameworkUnitOfWork(JobFinderDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ICompanySubscriptionsRepository CompanySubscriptionsRepository
        {
            get
            {
                this.companySubscriptionsRepository ??= new CompanySubscriptionsRepository(this.dbContext);

                return this.companySubscriptionsRepository;
            }
        }

        public IJobAdSubscriptionsRepository JobAdSubscriptionsRepository
        {
            get
            {
                this.jobAdSubscriptionsRepository ??= new JobAdSubscriptionsRepository(this.dbContext, this.mapper);

                return this.jobAdSubscriptionsRepository;
            }
        }

        public async Task SaveChanges()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
