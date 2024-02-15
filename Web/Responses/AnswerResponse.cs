namespace Web.Responses;

/// <summary>
/// Респонс ответа на вопрос
/// </summary>
public class AnswerResponse
{
    /// <summary>
    /// Идентификатор ответа
    /// </summary>
    public long AnswerId { get; set; }

    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public long QuestionId { get; set; }

    /// <summary>
    /// Содержание ответа
    /// </summary>
    public string Content { get; set; }
}