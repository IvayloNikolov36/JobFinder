using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.DataAccess.Implementations;

public class PersonalInfoRepository : EfCoreRepository<PersonalInfoEntity>, IPersonalInfoRepository
{
    private readonly IMapper mapper;

    public PersonalInfoRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task Update(PersonalInfoEditDTO personalInfoDto)
    {
        PersonalInfoEntity personalInfoFromDb = await this.DbSet.FindAsync(personalInfoDto.Id);

        base.ValidateForExistence(personalInfoFromDb, "PersonalInfo");

        this.mapper.Map(personalInfoDto, personalInfoFromDb);

        this.DbSet.Update(personalInfoFromDb);
    }

    public void Delete(string cvId)
    {
        base.DeleteWhere(x => x.CurriculumVitaeId == cvId);
    }
}
