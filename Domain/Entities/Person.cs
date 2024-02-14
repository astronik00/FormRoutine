namespace Domain.Entities;

public class Person
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}