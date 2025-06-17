using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Implementations.Cv
{
    public class SkillsInfosService : ISkillsInfosService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SkillsInfosService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Update(SkillsEditModel skillsInfoModel)
        {
            SkillsInfoEditDTO skillsInfoDto = this.mapper.Map<SkillsInfoEditDTO>(skillsInfoModel);

            await this.unitOfWork.SkillsInfoRepository.Update(skillsInfoDto);

            await this.unitOfWork.SaveChanges();
        }
    }
}
