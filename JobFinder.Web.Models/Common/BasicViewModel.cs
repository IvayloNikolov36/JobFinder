namespace JobFinder.Web.Models.Common
{
    public class BasicViewModel
    {
        public BasicViewModel(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public BasicViewModel(int id, string name)
        {
            this.Id = id.ToString();
            this.Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
