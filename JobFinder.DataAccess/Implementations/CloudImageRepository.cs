using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs;

namespace JobFinder.DataAccess.Implementations;

public class CloudImageRepository : EfCoreRepository<CloudImageEntity>, ICloudImageRepository
{
    private readonly IMapper mapper;

    public CloudImageRepository(
        JobFinderDbContext context,
        IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task Add(CloudImageDTO image)
    {
        CloudImageEntity cloudImageEntity = new();

        this.mapper.Map(image, cloudImageEntity);

        await this.DbSet.AddAsync(cloudImageEntity);
    }
}
