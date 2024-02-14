namespace Domain.Entities;

public class Question
{
    public long Id { get; set; }

    public long SurveyId { get; set; }

    public bool Mandatory { get; set; }

    public bool AllowedManyAnswers { get; set; }

    public int OrderNo { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public Survey Survey { get; set; } = null!;
}