namespace Domain.Entities;

public class Answer
{
    public long Id { get; set; }

    public long QuestionId { get; set; }

    public string Content { get; set; } = null!;

    public Question Question { get; set; } = null!;

    public ICollection<Result> Results { get; set; } = new List<Result>();
}