namespace Web.Requests;

/// <summary>
/// Параметры для сохранения ответа
/// </summary>
public class SaveResultRequest
{
    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    public long SessionId { get; init; }

    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public long QuestionId { get; init; }

    /// <summary>
    /// Список выбранных идентификаторов ответов
    /// </summary>
    public ICollection<long> AnswerIds { get; init; }
}