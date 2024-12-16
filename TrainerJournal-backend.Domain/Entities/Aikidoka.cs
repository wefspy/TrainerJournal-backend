namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность айкидоки (практикующего айкидо) в системе.
/// </summary>
public class Aikidoka : Entity<Guid>
{
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.UserIdentity"/>.
    /// </summary>
    public string UserName { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="UserName"/>
    /// </summary>
    public UserIdentity UserIdentity { get; private set; }

    /// <summary>
    /// Уровень кю (ранг).
    /// Представляет целочисленное значение, определяющее степень владения навыками айкидо.
    /// </summary>
    public int Kyu { get; set; }
}