using JobFinder.Data.Models;
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
        IMapFrom<LanguageLevelEntity>
    {
        public BasicViewModel(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public BasicViewModel(int id, string name)
        {
            this.Id = id.ToString();
            this.Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
