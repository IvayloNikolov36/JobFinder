namespace JobFinder.Services.Implementations
{
    using JobFinder.Data.Models.Nomenclature;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class JobCategoriesService : IJobCategoriesService
    {
        private readonly IRepository<JobCategoryEntity> repository;

        public JobCategoriesService(IRepository<JobCategoryEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> AllAsync<T>()
        {
            var dbCategories = await this.repository.AllAsNoTracking()
                .To<T>()
                .ToListAsync();

            return dbCategories;
        }
    }
}
