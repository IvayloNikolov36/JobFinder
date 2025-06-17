using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System;

namespace JobFinder.Web.Models.CvModels;

public class CvListingModel : IMapFrom<CVListingDTO>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool AnonymousProfileActivated { get; set; }
}
