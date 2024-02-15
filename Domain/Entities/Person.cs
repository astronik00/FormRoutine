namespace Domain.Entities;

/// <summary>
/// Данные прошедшего анкету
/// </summary>
public class Person
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Почтовый адрес
    /// </summary>
    public string Email { get; set; } = null!;

    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}