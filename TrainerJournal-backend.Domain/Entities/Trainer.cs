namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренера в системе.
/// Наследуется от <see cref="Entity{TKey}"/>, где в качестве TKey используется <see cref="Guid"/>.
/// </summary>
public class Trainer : Entity<Guid>
{
    /// <summary>
    /// Логин пользователя <see cref="UserIdentity"/>, авторизовавшегося в системе.
    /// </summary>
    public string UserName { get; init; }
}