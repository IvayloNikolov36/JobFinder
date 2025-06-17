using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Implementations.Cv
{
    public class WorkExperienceInfosService : IWorkExperienceInfosService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WorkExperienceInfosService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<UpdateResult> UpdateAsync(string cvId, IEnumerable<WorkExperienceEditModel> workExperienceModels)
        {
            IEnumerable<WorkExperienceInfoEditDTO> workExperienceInfoDTOs = this.mapper
                .Map<IEnumerable<WorkExperienceInfoEditDTO>>(workExperienceModels);

            IEnumerable<WorkExperienceInfoEditDTO> addedItems = await this.unitOfWork
                .WorkExperienceRepository
                .Update(cvId, workExperienceInfoDTOs);

            await this.unitOfWork.SaveChanges();

            return new UpdateResult(addedItems);
        }
    }
}
