namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренера в системе.
/// </summary>
public class Trainer(string userName) : Entity
{
    /// <summary>
    /// Ссылка на объекты внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<Group> Groups { get; private set; }
    
    /// <summary>
    /// Ссылка на объекты внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<Payment> Payments { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.UserIdentity"/>
    /// </summary>
    public string UserName { get; init; } = userName;

    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="UserName"/>
    /// </summary>
    public UserIdentity UserIdentity { get; private set; }
}