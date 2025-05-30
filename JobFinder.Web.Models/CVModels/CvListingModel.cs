using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;
using System;

namespace JobFinder.Web.Models.CVModels;

public class CvListingModel : IMapFrom<CVListingDTO>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool AnonymousProfileActivated { get; set; }
}
