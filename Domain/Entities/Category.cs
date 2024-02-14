namespace Domain.Entities;

public class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}