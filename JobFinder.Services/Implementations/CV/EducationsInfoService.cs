using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Implementations.Cv;

public class EducationsInfoService : IEducationsInfoService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public EducationsInfoService(
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
