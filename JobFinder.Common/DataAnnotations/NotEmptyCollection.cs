using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JobFinder.Common.DataAnnotations
{
    public class NotEmptyCollection : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as IReadOnlyCollection<object>;

            if (collection == null)
            {
                return false;
            }

            if (collection.Any())
            {
                return true;
            }

            return false;
        }
    }
}
