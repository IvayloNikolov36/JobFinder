﻿namespace JobFinder.Services.Implementations
{
    using JobFinder.Data.Models;
    using JobFinder.Data.Repositories;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class JobEngagementsService : IJobEngagementsService
    {
        private readonly IRepository<JobEngagement> repository;

        public JobEngagementsService(IRepository<JobEngagement> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> AllAsync<T>()
        {
            var dbEngagements = await this.repository.AllAsNoTracking()
                .To<T>()
                .ToListAsync();

            return dbEngagements;
        }
    }
}
