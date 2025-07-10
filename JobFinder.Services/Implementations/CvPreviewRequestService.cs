
using AutoMapper;
using JobFinder.Common.Exceptions;
using JobFinder.Data.Models.Cv;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Implementations;

public class CvPreviewRequestService : ICvPreviewRequestService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CvPreviewRequestService(
        IEntityFrameworkUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CvPreviewRequestListingViewModel>> GetAllCvPreviewRequests(
        string userId)
    {
        IEnumerable<CvPreviewRequestListingDTO> cvRequests = await this.unitOfWork
            .CvPreviewRequestRepository
            .GetAllCvPreviewRequests(userId);

        return this.mapper.Map<IEnumerable<CvPreviewRequestListingViewModel>>(cvRequests);
    }

    public async Task MakeCvPreviewRequest(
        CvPreviewRequestCreateViewModel requestModel,
        string currentUserId)
    {
        CvPreviewRequestDTO requestDto = this.mapper
            .Map<CvPreviewRequestDTO>(requestModel);

        requestDto.RequesterId = currentUserId;

        requestDto.CvId = await this.unitOfWork
            .AnonymousProfileRepository
            .GetCvId(requestModel.AnonymousProfileId.ToString());

        await this.unitOfWork
            .CvPreviewRequestRepository
            .MakeRequest(requestDto);

        await this.unitOfWork.SaveChanges();
    }

    public async Task AllowCvPreview(int id, string currentUserId)
    {
        await this.ValidateCvRequest(id, currentUserId);

        await this.unitOfWork.CvPreviewRequestRepository.AcceptRequest(id);

        await this.unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<CompanyCvPreviewRequestListingViewModel>> GetAllCompanyCvPreviewRequests(
        string userId)
    {
        IEnumerable<CompanyCvPreviewRequestListingDTO> requests = await this.unitOfWork
            .CvPreviewRequestRepository
            .GetCompanyCvRequests(userId);

        return this.mapper.Map<IEnumerable<CompanyCvPreviewRequestListingViewModel>>(requests);
    }

    private async Task ValidateCvRequest(int cvRequestId, string currentUserId)
    {
        CvPreviewRequestAcceptDataDTO requestAcceptData = await this.unitOfWork
            .CvPreviewRequestRepository
            .GetCvRequestAcceptData(cvRequestId);

        if (requestAcceptData.CvOwnerId!= currentUserId)
        {
            throw new ActionableException("You are not allowed to accept requests for access to foreign CVs!");
        }

        if (requestAcceptData.AcceptedDate.HasValue)
        {
            throw new ActionableException("You've already accepted the request!");
        }
    }
}
