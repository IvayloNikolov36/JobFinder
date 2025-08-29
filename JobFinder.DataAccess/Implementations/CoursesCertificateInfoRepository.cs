using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.Data.Models.Cv;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CoursesCertificateInfoRepository
    : EfCoreRepository<CourseCertificateEntity>, ICoursesCertificateInfoRepository
{
    private readonly IMapper mapper;

    public CoursesCertificateInfoRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CourseCertificateDTO>> All(string cvId)
    {
        return await this.DbSet.AsNoTracking()
            .Where(cs => cs.CvId == cvId)
            .To<CourseCertificateDTO>()
            .ToListAsync();
    }

    public async Task<IEnumerable<CourseCertificateSimpleDTO>> Update(
        string cvId,
        IEnumerable<CourseCertificateSimpleDTO> courcesInfo)
    {
        List<CourseCertificateEntity> courceEntitiesFromDb = await this.DbSet
            .Where(we => we.CvId == cvId)
            .ToListAsync();

        IEnumerable<CourseCertificateSimpleDTO> coursesToAdd = courcesInfo
            .Where(m => !courceEntitiesFromDb.Any(ce => ce.Id == m.Id));

        List<CourseCertificateEntity> entitiesToAdd = null;
        if (coursesToAdd.Any())
        {
            entitiesToAdd = new List<CourseCertificateEntity>();
            foreach (CourseCertificateSimpleDTO model in coursesToAdd)
            {
                CourseCertificateEntity entityToAdd = this.mapper.Map<CourseCertificateEntity>(model);
                entityToAdd.Id = 0;
                entityToAdd.CvId = cvId;
                entitiesToAdd.Add(entityToAdd);
            }

            await this.DbSet.AddRangeAsync(entitiesToAdd);
        }

        IEnumerable<CourseCertificateEntity> entitiesToRemove = courceEntitiesFromDb
            .Where(ce => !courcesInfo.Any(m => m.Id == ce.Id));

        if (entitiesToRemove.Any())
        {
            base.DeleteRange(entitiesToRemove);
        }

        IEnumerable<CourseCertificateEntity> entitiesToUpdate = courceEntitiesFromDb
            .Where(ce => courcesInfo.Any(m => m.Id == ce.Id));

        if (entitiesToUpdate.Any())
        {
            foreach (CourseCertificateEntity item in entitiesToUpdate)
            {
                CourseCertificateSimpleDTO correspondingModel = courcesInfo
                    .First(m => m.Id == item.Id);

                this.mapper.Map(correspondingModel, item);
            }

            this.DbSet.UpdateRange(entitiesToUpdate);
        }

        return this.mapper.Map<IEnumerable<CourseCertificateSimpleDTO>>(entitiesToAdd);
    }

    public async Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> courseCertificateInfoIds)
    {
        CourseCertificateEntity[] courses = await this.DbSet
            .Where(c => c.CvId == cvId)
            .ToArrayAsync();

        foreach (CourseCertificateEntity course in courses)
        {
            course.IncludeInAnonymousProfile = courseCertificateInfoIds.Contains(course.Id);
        }

        this.DbSet.UpdateRange(courses);
    }

    public async Task DisassociateFromAnonymousProfile(string cvId)
    {
        CourseCertificateEntity[] courses = await this.DbSet
            .Where(c => c.CvId == cvId)
            .ToArrayAsync();

        foreach (CourseCertificateEntity course in courses)
        {
            course.IncludeInAnonymousProfile = null;
        }

        this.DbSet.UpdateRange(courses);
    }

    public async Task Delete(int id)
    {
        CourseCertificateEntity courseCertificateFromDb = await this.DbSet.FindAsync(id);

        base.ValidateForExistence(courseCertificateFromDb, nameof(CourseCertificateEntity));

        this.DbSet.Remove(courseCertificateFromDb);
    }

    public void Delete(string cvId)
    {
        base.DeleteWhere(cs => cs.CvId == cvId);
    }
}
