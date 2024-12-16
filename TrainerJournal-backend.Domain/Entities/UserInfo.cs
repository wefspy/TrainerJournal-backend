using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность описания пользователя
/// </summary>
public class UserInfo : Entity<Guid>
{
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.UserIdentity"/>
    /// </summary>
    public string UserName { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="UserName"/>
    /// </summary>
    public UserIdentity UserIdentity { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.PersonName"/>
    /// </summary>
    public Guid PersonNameId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="PersonNameId"/>
    /// </summary>
    public PersonName PersonName { get; private set; }
    
    /// <summary>
    /// Пол пользователя
    /// </summary>
    public Gender Gender { get; set; }
}