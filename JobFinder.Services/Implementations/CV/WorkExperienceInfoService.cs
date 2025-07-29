using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.Extensions.Caching.Distributed;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations.Cv
{
    public class WorkExperienceInfoService : IWorkExperienceInfoService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;

        public WorkExperienceInfoService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            IDistributedCache distributedCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public async Task<UpdateResult> UpdateAsync(
            string cvId,
            IEnumerable<WorkExperienceEditModel> workExperienceModels)
        {
            IEnumerable<WorkExperienceInfoEditDTO> workExperienceInfoDTOs = this.mapper
                .Map<IEnumerable<WorkExperienceInfoEditDTO>>(workExperienceModels);

            IEnumerable<WorkExperienceInfoEditDTO> addedItems = await this.unitOfWork
                .WorkExperienceRepository
                .Update(cvId, workExperienceInfoDTOs);

            await this.unitOfWork.SaveChanges();

            string cacheKey = string.Format(CvCacheKey, cvId);

            await this.distributedCache.RemoveAsync(cacheKey);

            return new UpdateResult(addedItems);
        }
    }
}
