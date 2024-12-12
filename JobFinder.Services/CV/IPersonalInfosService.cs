namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.CVModels;
    using System.Threading.Tasks;

    public interface IPersonalInfosService
    {
        Task<bool> UpdateAsync(PersonalDetailsEditModel personalDetails);
    }
}
