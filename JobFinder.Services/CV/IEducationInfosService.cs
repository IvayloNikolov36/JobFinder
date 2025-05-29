namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEducationInfosService
    {
        Task<UpdateResult> Update(string cvId, IEnumerable<EducationEditModel> educationsModel);
    }
}
