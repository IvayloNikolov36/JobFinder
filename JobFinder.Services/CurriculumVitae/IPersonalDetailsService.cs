namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Web.Models.CVModels;
    using System;
    using System.Threading.Tasks;

    public interface IPersonalDetailsService
    {
        Task<T> GetAsync<T>(string cvId);

        //Task<int> CreateAsync(
        //    string cvId,
        //    string firstName,
        //    string middleName,
        //    string lastName,
        //    DateTime birthdate,
        //    GenderEnum gender,
        //    string email,
        //    string phone,
        //    CountryEnum country,
        //    CountryEnum citizenShip,
        //    string city);

        Task<bool> UpdateAsync(string cvId, PersonalDetailsEditModel personalDetails);
    }
}
