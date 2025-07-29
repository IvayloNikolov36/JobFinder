using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.CvModels;
using Microsoft.Extensions.Caching.Distributed;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations.Cv
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;

        public PersonalInfoService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            IDistributedCache distributedCache) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public async Task Update(string cvId, PersonalInfoEditModel personalInfo)
        {
            PersonalInfoEditDTO personalInfoDto = this.mapper.Map<PersonalInfoEditDTO>(personalInfo);

            await this.unitOfWork.PersonalInfoRepository.Update(personalInfoDto);

            await this.unitOfWork.SaveChanges();

            string cacheKey = string.Format(CvCacheKey, cvId);

            await this.distributedCache.RemoveAsync(cacheKey);
        }
    }
}
