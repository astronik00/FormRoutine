namespace Domain.Entities;

/// <summary>
/// Информация об анкете
/// </summary>
public class Survey
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Имя анкеты
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Количество вопросов в анкете
    /// </summary>
    public int QuestionNumber { get; set; }

    /// <summary>
    /// Описание анкеты
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Признак возможности пройти анкету
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Признак возможности пройти анкету только один раз
    /// </summary>
    public bool OnlyOnce { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}