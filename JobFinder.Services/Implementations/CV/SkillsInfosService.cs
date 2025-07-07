using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.CvModels;
using Microsoft.Extensions.Caching.Distributed;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations.Cv
{
    public class SkillsInfosService : ISkillsInfosService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;

        public SkillsInfosService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            IDistributedCache distributedCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public async Task Update(string cvId, SkillsEditModel skillsInfoModel)
        {
            SkillsInfoEditDTO skillsInfoDto = this.mapper.Map<SkillsInfoEditDTO>(skillsInfoModel);

            await this.unitOfWork.SkillsInfoRepository.Update(skillsInfoDto);

            await this.unitOfWork.SaveChanges();

            string cacheKey = string.Format(CvCacheKey, cvId);

            await this.distributedCache.RemoveAsync(cacheKey);
        }
    }
}
