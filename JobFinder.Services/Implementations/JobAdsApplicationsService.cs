using AutoMapper;
using JobFinder.Common.Exceptions;
using JobFinder.Data.Models;
using JobFinder.Data.Repositories.Contracts;
using JobFinder.Services.Mappings;
using JobFinder.Web.Models.JobAds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Services.Implementations
{
    public class JobAdsApplicationsService : IJobAdsApplicationsService
    {
        private readonly IMapper mapper;
        private readonly IRepository<JobAdApplicationEntity> jobAdsApplicationsRepository;
        private readonly IRepository<JobAdvertisementEntity> jobAdRepository;

        public JobAdsApplicationsService(
            IMapper mapper,
            IRepository<JobAdApplicationEntity> jobAdsApplicationsRepository,
            IRepository<JobAdvertisementEntity> jobAdRepository)
        {
            this.mapper = mapper;
            this.jobAdsApplicationsRepository = jobAdsApplicationsRepository;
            this.jobAdRepository = jobAdRepository;
        }

        public async Task Create(JobAdApplicationInputModel jobAdApplication)
        {
            bool hasAlreadyApplied = await this.jobAdsApplicationsRepository
                .ExistAsync(j => j.ApplicantId == jobAdApplication.ApplicantId
                    && j.JobAdId == jobAdApplication.JobAdId);

            if (hasAlreadyApplied)
            {
                throw new ActionableException("You can't applie again for this job ad!");
            }

            JobAdApplicationEntity newJobAdApplicationEntity = this.mapper.Map<JobAdApplicationEntity>(jobAdApplication);

            newJobAdApplicationEntity.AppliedOn = DateTime.UtcNow;

            await this.jobAdsApplicationsRepository.AddAsync(newJobAdApplicationEntity);

            await this.jobAdsApplicationsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobAdApplicationViewModel>> GetJobAdApplications(int jobAdId, string userId)
        {
            string jobAdPublisherId = await this.jobAdRepository
                .Where(ja => ja.Id == jobAdId)
                .Select(ja => ja.Publisher.UserId)
                .SingleOrDefaultAsync();

            if (jobAdPublisherId == null)
            {
                throw new ActionableException($"Job Advertisement with id {jobAdId} does not exist!");
            }

            if (jobAdPublisherId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to access data for other users job advertisements!");
            }

            return await this.jobAdsApplicationsRepository.AllAsNoTracking()
                .Where(j => j.JobAdId == jobAdId)
                .OrderByDescending(j => j.AppliedOn)
                .To<JobAdApplicationViewModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<JobAdApplicationViewModel>> GetAllMine(string userId)
        {
            return await this.jobAdsApplicationsRepository.AllAsNoTracking()
                .Where(j => j.ApplicantId == userId)
                .OrderByDescending(j => j.AppliedOn)
                .To<JobAdApplicationViewModel>()
                .ToListAsync();
        }
    }
}
