namespace Web.Requests;

public class SaveResultRequest
{
    public long SessionId { get; init; }

    public long QuestionId { get; init; }

    public ICollection<long> AnswerIds { get; init; }
}