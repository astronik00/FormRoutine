namespace Domain.Entities;

/// <summary>
/// Категория анкеты
/// </summary>
public class Category
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; set; } = null!;

    public ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}