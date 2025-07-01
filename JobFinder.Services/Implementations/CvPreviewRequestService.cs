
using JobFinder.Common.Exceptions;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Services.Implementations;

public class CvPreviewRequestService : ICvPreviewRequestService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;

    public CvPreviewRequestService(IEntityFrameworkUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;   
    }

    public async Task AllowCvPreview(int id, string currentUserId)
    {
        await this.ValidateCvRequest(id, currentUserId);

        await this.unitOfWork.CvPreviewRequestRepository.AcceptRequest(id);

        await this.unitOfWork.SaveChanges();
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
