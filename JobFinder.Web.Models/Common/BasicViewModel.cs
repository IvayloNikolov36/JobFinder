using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Web.Models.Common;

public class BasicViewModel : IMapFrom<BasicDTO>
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
