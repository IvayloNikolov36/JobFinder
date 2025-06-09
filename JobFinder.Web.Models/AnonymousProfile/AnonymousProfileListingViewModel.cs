using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using System;

namespace JobFinder.Web.Models.AnonymousProfile;

public class AnonymousProfileListingViewModel : IMapFrom<AnonymousProfileListingDTO>
{
    public int Id { get; set; }

    public DateTime ActivateDate { get; set; }
}
