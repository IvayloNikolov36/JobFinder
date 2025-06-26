using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.Cv;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.DataAccess.Implementations;

public class CvPreviewRequestRepository : EfCoreRepository<CvPreviewRequestEntity>, ICvPreviewRequestRepository
{
    private readonly IMapper mapper;

    public CvPreviewRequestRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task CreateRequest(CvPreviewRequestDTO request)
    {
        CvPreviewRequestEntity requestEntity = new CvPreviewRequestEntity();
        this.mapper.Map(request, requestEntity);
        requestEntity.RequestDate = DateTime.UtcNow;

        await this.DbSet.AddAsync(requestEntity);
    }
}
