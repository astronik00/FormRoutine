namespace Web.Responses;

/// <summary>
/// Респонс вопроса
/// </summary>
public class QuestionResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор анкеты
    /// </summary>
    public long SurveyId { get; set; }

    /// <summary>
    /// Содержание вопроса
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Порядковый номер вопроса
    /// </summary>
    public int OrderNo { get; set; }

    /// <summary>
    /// Признак обязательности вопроса
    /// </summary>
    public bool Mandatory { get; set; }

    /// <summary>
    /// Признак возможности множественных ответов
    /// </summary>
    public bool AllowedManyAnswers { get; set; }

    /// <summary>
    /// Список ответов на данный вопрос
    /// </summary>
    public ICollection<AnswerResponse> Answers { get; set; }
}