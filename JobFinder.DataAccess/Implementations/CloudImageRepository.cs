using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs;
using Microsoft.EntityFrameworkCore;

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

    public async Task Update(string userId, CloudImageDTO imageDto)
    {
        CloudImageEntity image = await this.DbSet
            .Where(ci => ci.UserId == userId)
            .SingleOrDefaultAsync();

        if (image is null)
        {
            return;
        }

        this.mapper.Map(imageDto, image);

        this.DbSet.Update(image);
    }

    public async Task<string> GetUrl(int pictureId)
    {
        string url = await this.DbSet
            .Where(ci => ci.Id == pictureId)
            .Select(ci => ci.Url)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(url, nameof(CloudImageEntity));

        return url;
    }

    public async Task<string> GetThumbnailUrl(int pictureId)
    {
        string url = await this.DbSet
            .Where(ci => ci.Id == pictureId)
            .Select(ci => ci.ThumbnailUrl)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(url, nameof(CloudImageEntity));

        return url;
    }
}
