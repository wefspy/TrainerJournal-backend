using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность описания пользователя
/// </summary>
public class UsersInfo : Entity<Guid>
{
    /// <summary>
    /// Логин пользователя <see cref="UsersIdentity"/>, авторизовавшегося в системе
    /// </summary>
    public string UserName { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор имени пользователя <see cref="PeopleNames"/>
    /// </summary>
    public Guid PersonNameId { get; init; }
    
    /// <summary>
    /// Пол пользователя
    /// </summary>
    public Gender Gender { get; init; }
}