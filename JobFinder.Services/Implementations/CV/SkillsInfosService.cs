using AutoMapper;
using JobFinder.Data.Models.Cv;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CV;
using JobFinder.Transfer.DTOs.CV;
using JobFinder.Web.Models.CVModels;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Services.Implementations.CV
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
