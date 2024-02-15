namespace Domain.Entities;

/// <summary>
/// Сессия
/// </summary>
public class Interview
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
    /// Идентификатор человека
    /// </summary>
    public long? PersonId { get; set; }

    /// <summary>
    /// Начало прохождения анкеты
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Конец прохождения анкеты
    /// </summary>
    public DateTime? EndTime { get; set; }

    public ICollection<Result> Results { get; set; } = new List<Result>();

    public Person PersonNavigation { get; set; } = null!;

    public Survey SurveyNavigation { get; set; } = null!;
}