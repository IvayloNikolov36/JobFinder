namespace JobFinder.Transfer.DTOs;

public class BasicDTO
{
    public BasicDTO()
    {

    }

    public BasicDTO(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }
}
