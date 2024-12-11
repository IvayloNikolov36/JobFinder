using JobFinder.Data.Models.Common;
using System.Collections.Generic;
using System.Linq;

namespace JobFinder.Web.Models.Common
{
    public class UpdateResult
    {
        public UpdateResult(IEnumerable<BaseEntity<int>> entities)
        {
            this.NewItemsIds = entities == null
                ? Enumerable.Empty<int>()
                : entities.Select(x => x.Id);
        }

        public IEnumerable<int> NewItemsIds { get; }
    }
}
