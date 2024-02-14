namespace Domain.Entities;

public class Survey
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public int QuestionNumber { get; set; }

    public string Description { get; set; } = null!;

    public bool Active { get; set; }

    public bool OnlyOnce { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}