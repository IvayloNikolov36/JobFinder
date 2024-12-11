namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using System.Collections.Generic;
    using System.Linq;

    public class SkillsViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ComputerSkills { get; set; }

        public string OtherSkills { get; set; }

        public bool HasManagedPeople { get; set; }

        public bool HasDrivingLicense { get; set; }

        public IEnumerable<BasicViewModel> DrivingLicenseCategories { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SkillsInfoEntity, SkillsViewModel>()
                .ForMember(vm => vm.DrivingLicenseCategories, o => o.MapFrom(e => e.SkillsInfoDrivingCategories
                    .Select(sdc => sdc.DrivingCategory)));
        }
    }
}
