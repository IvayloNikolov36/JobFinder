using AutoMapper;
using JobFinder.Data;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Implementations;
using JobFinder.Transfer.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JobFinder.DataAccess.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : IEntityFrameworkUnitOfWork
    {
        private readonly JobFinderDbContext dbContext;
        private readonly IMapper mapper;

        private ICompanySubscriptionsRepository companySubscriptionsRepository;
        private IJobAdSubscriptionsRepository jobAdSubscriptionsRepository;
        private ICurriculumVitaeRepository curriculumVitaeRepository;
        private IWorkExperienceRepository workExperienceRepository;
        private IEducationInfoRepository educationInfoRepository;
        private ILanguageInfoRepository languageInfoRepository;
        private ICoursesCertificateInfoRepository coursesCertificateInfoRepository;

        public EntityFrameworkUnitOfWork(JobFinderDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ICompanySubscriptionsRepository CompanySubscriptionsRepository
        {
            get
            {
                this.companySubscriptionsRepository ??= new CompanySubscriptionsRepository(this.dbContext);
                return this.companySubscriptionsRepository;
            }
        }

        public IJobAdSubscriptionsRepository JobAdSubscriptionsRepository
        {
            get
            {
                this.jobAdSubscriptionsRepository ??= new JobAdSubscriptionsRepository(this.dbContext, this.mapper);
                return this.jobAdSubscriptionsRepository;
            }
        }

        public ICurriculumVitaeRepository CurriculumVitaeRepository
        {
            get
            {
                this.curriculumVitaeRepository ??= new CurriculumVitaeRepository(this.dbContext, mapper);
                return this.curriculumVitaeRepository;
            }
        }

        public IWorkExperienceRepository WorkExperienceRepository
        {
            get
            {
                this.workExperienceRepository ??= new WorkExperienceRepository(this.dbContext);
                return this.workExperienceRepository;
            }
        }

        public IEducationInfoRepository EducationInfoRepository
        {
            get
            {
                this.educationInfoRepository ??= new EducationInfoRepository(this.dbContext);
                return this.educationInfoRepository;
            }
        }

        public ILanguageInfoRepository LanguageInfoRepository
        {
            get
            {
                this.languageInfoRepository ??= new LanguageInfoRepository(this.dbContext);
                return this.languageInfoRepository;
            }
        }

        public ICoursesCertificateInfoRepository CoursesCertificateInfoRepository
        {
            get
            {
                this.coursesCertificateInfoRepository ??= new CoursesCertificateInfoRepository(this.dbContext);
                return this.coursesCertificateInfoRepository;
            }
        }

        public async Task SaveChanges()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges<DTO, IdType>(DTO dtoToPopulateId) where DTO : IUniquelyIdentified<IdType>
        {
            await this.SaveChanges<DTO, IdType>(new List<DTO>(1) { dtoToPopulateId });
        }

        public async Task SaveChanges<DTO, IdType>(IEnumerable<DTO> dtosToPopulateId) where DTO : IUniquelyIdentified<IdType>
        {
            List<IAuditInfo<IdType>> entitiesToInsert = this.dbContext.ChangeTracker
                .Entries<IAuditInfo<IdType>>()
                .Where(entity => entity.State != EntityState.Detached)
                .Select(entity => entity.Entity)
                .ToList();

            await this.SaveChanges();

            foreach (DTO dto in dtosToPopulateId)
            {
                this.Traverse(entitiesToInsert, dto);
            }
        }

        private void Traverse<IdType>(IList<IAuditInfo<IdType>> entitiesToInsert, IUniquelyIdentified<IdType> dto)
        {
            IAuditInfo<IdType> entity = entitiesToInsert
                .FirstOrDefault(e => e.UniqueIdentificator == dto.UniqueIdentificator);

            if (entity != null)
            {
                dto.Id = entity.Id;
            }


            if (typeof(IAuditInfo<IdType>).IsAssignableFrom(dto.GetType()))
            {
                IAuditInfo<IdType> auditDto = dto as IAuditInfo<IdType>;
                auditDto.CreatedOn = entity.CreatedOn;
                auditDto.ModifiedOn = entity.ModifiedOn;
            }

            IEnumerable<IUniquelyIdentified<IdType>> dtoChildren = this.GetAllChildren(dto);

            foreach (IUniquelyIdentified<IdType> dtoChild in dtoChildren)
            {
                this.Traverse(entitiesToInsert, dtoChild);
            }
        }

        private IEnumerable<IUniquelyIdentified<IdType>> GetAllChildren<IdType>(IUniquelyIdentified<IdType> dto)
        {
            IEnumerable<IUniquelyIdentified<IdType>> single = dto.GetType()
                .GetProperties()
                .Where(p => p.MemberType == MemberTypes.Property
                    && typeof(IUniquelyIdentified<IdType>).IsAssignableFrom(p.PropertyType)
                    && p.GetValue(dto, null) != null)
                .Select(p => (IUniquelyIdentified<IdType>)p.GetValue(dto, null));

            IEnumerable<IUniquelyIdentified<IdType>> enumerable = dto.GetType()
                .GetProperties()
                .Where(p => p.MemberType == MemberTypes.Property
                    && typeof(IEnumerable<IUniquelyIdentified<IdType>>).IsAssignableFrom(p.PropertyType)
                    && p.GetValue(dto, null) != null)
                .SelectMany(p => (IEnumerable<IUniquelyIdentified<IdType>>)p.GetValue(dto, null));

            return single.Concat(enumerable).ToList();
        }
    }
}
