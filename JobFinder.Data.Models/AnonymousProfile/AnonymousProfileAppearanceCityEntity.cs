using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;

namespace JobFinder.Data.Models.AnonymousProfile
{
    public class AnonymousProfileAppearanceCityEntity : BaseEntity<int>
    {
        public int AnonymousProfileAppearanceId { get; set; }
        public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

        public int CityId { get; set; }
        public CityEntity City { get; set; }
    }
}
