namespace Domain.Entities;

/// <summary>
/// Ответ на вопрос
/// </summary>
public class Answer
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public long QuestionId { get; set; }

    /// <summary>
    /// Содержание ответа
    /// </summary>
    public string Content { get; set; } = null!;

    public Question Question { get; set; } = null!;

    public ICollection<Result> Results { get; set; } = new List<Result>();
}