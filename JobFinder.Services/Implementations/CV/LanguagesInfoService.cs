using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.Cv;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.Extensions.Caching.Distributed;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations.Cv;

public class LanguagesInfoService : ILanguagesInfoService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IDistributedCache distributedCache;

    public LanguagesInfoService(
        IEntityFrameworkUnitOfWork unitOfWork,
        IMapper mapper,
        IDistributedCache distributedCache)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.distributedCache = distributedCache;
    }

    public async Task<UpdateResult> Update(
        string cvId,
        IEnumerable<LanguageInfoEditModel> languageInfoModels)
    {
        IEnumerable<LanguageInfoEditDTO> languageInfoDtos = this.mapper
            .Map<IEnumerable<LanguageInfoEditDTO>>(languageInfoModels);

        IEnumerable<LanguageInfoEditDTO> addedItems = await this.unitOfWork
            .LanguageInfoRepository
            .Update(cvId, languageInfoDtos);

        await this.unitOfWork.SaveChanges();

        string cacheKey = string.Format(CvCacheKey, cvId);

        await this.distributedCache.RemoveAsync(cacheKey);

        return new UpdateResult(addedItems);
    }
}
