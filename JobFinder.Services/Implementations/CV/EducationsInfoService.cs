﻿using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.Extensions.Caching.Distributed;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations.Cv;

public class EducationsInfoService : IEducationsInfoService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IDistributedCache distributedCache;

    public EducationsInfoService(
        IEntityFrameworkUnitOfWork unitOfWork,
        IMapper mapper,
        IDistributedCache distributedCache) 
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.distributedCache = distributedCache;
    }
    
    public async Task<UpdateResult> Update(string cvId, IEnumerable<EducationEditModel> educationModels)
    {
        IEnumerable<EducationInfoEditDTO> educationDtos = this.mapper
            .Map<IEnumerable<EducationInfoEditDTO>>(educationModels);

        IEnumerable<EducationInfoEditDTO> itemsAdded = await this.unitOfWork
            .EducationInfoRepository
            .Update(cvId, educationDtos);
                       
        await this.unitOfWork.SaveChanges();

        string cacheKey = string.Format(CvCacheKey, cvId);

        await this.distributedCache.RemoveAsync(cacheKey);

        return new UpdateResult(itemsAdded);
    }
}
