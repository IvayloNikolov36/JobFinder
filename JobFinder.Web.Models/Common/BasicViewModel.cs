using JobFinder.Data.Models.Nomenclature;
using JobFinder.Services.Mappings;

namespace JobFinder.Web.Models.Common
{
    public class BasicViewModel :  
        IMapFrom<CountryEntity>,
        IMapFrom<CitizenshipEntity>,
        IMapFrom<GenderEntity>,
        IMapFrom<EducationLevelEntity>,
        IMapFrom<BusinessSectorEntity>,
        IMapFrom<LanguageTypeEntity>,
        IMapFrom<LanguageLevelEntity>,
        IMapFrom<DrivingCategoryEntity>,
        IMapFrom<JobEngagementEntity>,
        IMapFrom<JobCategoryEntity>,
        IMapFrom<CityEntity>,
        IMapFrom<CurrencyEntity>
    {
        public BasicViewModel()
        {

        }

        public BasicViewModel(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
