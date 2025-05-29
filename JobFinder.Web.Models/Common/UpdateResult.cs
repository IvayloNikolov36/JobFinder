using JobFinder.Transfer.Common;
using System.Collections.Generic;
using System.Linq;

namespace JobFinder.Web.Models.Common;

public class UpdateResult
{
    public UpdateResult(IEnumerable<IIdentity> entities)
    {
        this.NewItemsIds = entities == null
            ? []
            : [..entities.Select(x => x.Id)];
    }

    public IEnumerable<int> NewItemsIds { get; }
}
