namespace JobFinder.Web.Models.Common;

public class IdentityViewModel<T>
{

    public IdentityViewModel(T id)
    {
        this.Id = id;
    }

    public T Id { get; }
}
