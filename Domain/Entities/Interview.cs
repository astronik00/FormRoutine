namespace Domain.Entities;

public class Interview
{
    public long Id { get; set; }

    public long SurveyId { get; set; }

    public long? PersonId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public ICollection<Result> Results { get; set; } = new List<Result>();

    public Person PersonNavigation { get; set; } = null!;

    public Survey SurveyNavigation { get; set; } = null!;
}