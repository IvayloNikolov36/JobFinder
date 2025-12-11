using JobFinder.Transfer.Common;
using System.Collections.Generic;
using System.Linq;

namespace JobFinder.Web.Models.Common;

public class UpdateResult<T>
{
    public UpdateResult(IEnumerable<IUniquelyIdentified<T>> entities)
    {
        this.NewItemsIds = entities == null
            ? []
            : [..entities.Select(x => x.Id)];
    }

    public IEnumerable<T> NewItemsIds { get; }
}
