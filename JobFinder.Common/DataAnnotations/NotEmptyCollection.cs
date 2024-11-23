using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JobFinder.Common.DataAnnotations
{
    public class NotEmptyCollection : ValidationAttribute
    {
        // TODO: fix the annotation to not throw 50x
        public override bool IsValid(object value)
        {
            var collection = value as IReadOnlyCollection<object>;

            if (collection.Any())
            {
                return true;
            }

            return false;
        }
    }
}
