namespace Web.Responses;

public class QuestionResponse
{
    public long Id { get; set; }

    public long SurveyId { get; set; }

    public string Name { get; set; }

    public int OrderNo { get; set; }

    public bool Mandatory { get; set; }

    public bool AllowedManyAnswers { get; set; }

    public ICollection<AnswerResponse> Answers { get; set; }
}

// public record QuestionResponse(long Id, long SurveyId)