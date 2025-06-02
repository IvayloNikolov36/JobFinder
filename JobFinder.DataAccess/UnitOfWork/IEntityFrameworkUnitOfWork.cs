using JobFinder.DataAccess.Contracts;
using JobFinder.Transfer.Common;

namespace JobFinder.DataAccess.UnitOfWork
{
    public interface IEntityFrameworkUnitOfWork
    {
        ICompanySubscriptionsRepository CompanySubscriptionsRepository { get; }

        IJobAdSubscriptionsRepository JobAdSubscriptionsRepository { get; }

        ICurriculumVitaeRepository CurriculumVitaeRepository { get; }

        IPersonalInfoRepository PersonalInfoRepository { get; }

        IWorkExperienceRepository WorkExperienceRepository { get; }

        IEducationInfoRepository EducationInfoRepository { get; }

        ILanguageInfoRepository LanguageInfoRepository { get; }

        ICoursesCertificateInfoRepository CoursesCertificateInfoRepository {  get; }

        ISkillsInfoDrivingCategoryRepository SkillsInfoDrivingCategoryRepository { get; }

        ISkillsInfoRepository SkillsInfoRepository { get; }

        IJobAdApplicationsRepository JobAdApplicationsRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IJobAdRepository JobAdRepository { get; }

        IUserRepository UserRepository { get; }

        Task SaveChanges();

        Task SaveChanges<DTO, IdType>(DTO dtoToPopulateId) where DTO : IUniquelyIdentified<IdType>;

        Task SaveChanges<DTO, IdType>(IEnumerable<DTO> dtosToPopulateId) where DTO : IUniquelyIdentified<IdType>;
    }
}
