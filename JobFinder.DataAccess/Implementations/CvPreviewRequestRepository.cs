using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.Cv;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CvPreviewRequestRepository : EfCoreRepository<CvPreviewRequestEntity>, ICvPreviewRequestRepository
{
    private readonly IMapper mapper;

    public CvPreviewRequestRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task MakeRequest(CvPreviewRequestDTO request)
    {
        CvPreviewRequestEntity requestEntity = new();
        this.mapper.Map(request, requestEntity);
        requestEntity.RequestDate = DateTime.UtcNow;

        await this.DbSet.AddAsync(requestEntity);
    }

    public async Task AcceptRequest(int id)
    {
        CvPreviewRequestEntity cvPreviewRequest = await this.DbSet.FindAsync(id);

        base.ValidateForExistence(cvPreviewRequest, nameof(CvPreviewRequestEntity));

        cvPreviewRequest.AcceptedDate = DateTime.UtcNow;

        this.DbSet.Update(cvPreviewRequest);
    }

    public async Task<CvPreviewRequestAcceptDataDTO> GetCvRequestAcceptData(int cvRequestId)
    {
        CvPreviewRequestAcceptDataDTO data = await this.DbSet.AsNoTracking()
            .Where(r => r.Id == cvRequestId)
            .Select(r => new CvPreviewRequestAcceptDataDTO
            {
                CvOwnerId = r.AnonymousProfile.UserId,
                AcceptedDate = r.AcceptedDate
            })
            .SingleOrDefaultAsync();

        base.ValidateForExistence(data, nameof(CvPreviewRequestEntity));

        return data;
    }

    public async Task<IEnumerable<CompanyCvPreviewRequestListingDTO>> GetCompanyCvRequests(string userId)
    {
        return await this.DbSet.AsNoTracking()
            .Where(cpr => cpr.RequesterId == userId)
            .To<CompanyCvPreviewRequestListingDTO>()
            .ToListAsync();
    }
}
