namespace Domain.Entities;

/// <summary>
/// Данные ответа на вопрос
/// </summary>
public class Result
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    public long InterviewId { get; set; }

    /// <summary>
    /// Идентификатор ответа на вопрос
    /// </summary>
    public long AnswerId { get; set; }

    public Answer Answer { get; set; } = null!;

    public Interview Interview { get; set; } = null!;
}