using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;

public class 
    JobSubscriptionViewModel : IHaveCustomMappings
{
    private const string NoPropValueSubstitution = "All";

    public int Id { get; set; }

    public string RecurringType { get; set; }

    public string JobCategory { get; set; }

    public string JobEngagement { get; set; }

    public string Location { get; set; }

    public bool SpecifiedSalary { get; set; }

    public bool Intership { get; set; }

    public string SearchTerm { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<JobSubscriptionDTO, JobSubscriptionViewModel>()
            .ForMember(vm => vm.JobCategory, o => o.MapFrom(dto => dto.JobCategory ?? NoPropValueSubstitution))
            .ForMember(vm => vm.JobEngagement, o => o.MapFrom(dto => dto.JobEngagement ?? NoPropValueSubstitution))
            .ForMember(vm => vm.Location, o => o.MapFrom(dto => dto.Location ?? NoPropValueSubstitution));
    }
}
