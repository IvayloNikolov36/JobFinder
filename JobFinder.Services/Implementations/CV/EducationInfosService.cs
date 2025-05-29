using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.CV;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.Implementations.CV;

public class EducationInfosService : IEducationInfosService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public EducationInfosService(
        IEntityFrameworkUnitOfWork unitOfWork,
        IMapper mapper) 
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<UpdateResult> Update(string cvId, IEnumerable<EducationEditModel> educationModels)
    {
        IEnumerable<EducationInfoEditDTO> educationDtos = this.mapper
            .Map<IEnumerable<EducationInfoEditDTO>>(educationModels);

        IEnumerable<EducationInfoEditDTO> itemsAdded = await this.unitOfWork
            .EducationInfoRepository
            .Update(cvId, educationDtos);
                       
        await this.unitOfWork.SaveChanges();

        return new UpdateResult(itemsAdded);
    }
}
