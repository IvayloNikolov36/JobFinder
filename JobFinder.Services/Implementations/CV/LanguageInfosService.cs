using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.CV;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.Implementations.CV;

public class LanguageInfosService : ILanguageInfosService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public LanguageInfosService(
        IEntityFrameworkUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
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

        return new UpdateResult(addedItems);
    }
}
