namespace JobFinder.Transfer.Common
{
    public interface IUniquelyIdentified<IdType>
    {
        public IdType Id { get; set; }

        Guid UniqueIdentificator { get; set; }
    }
}
