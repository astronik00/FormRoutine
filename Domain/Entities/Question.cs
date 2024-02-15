namespace Domain.Entities;

/// <summary>
/// Вопрос анкеты
/// </summary>
public class Question
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
    /// Признак обязательности вопроса
    /// </summary>
    public bool Mandatory { get; set; }

    /// <summary>
    /// Признак возможности дать несколько ответов
    /// </summary>
    public bool AllowedManyAnswers { get; set; }

    /// <summary>
    /// Порядковый номер вопроса в анкете
    /// </summary>
    public int OrderNo { get; set; }

    /// <summary>
    /// Содержание вопроса
    /// </summary>
    public string Name { get; set; } = null!;

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public Survey Survey { get; set; } = null!;
}