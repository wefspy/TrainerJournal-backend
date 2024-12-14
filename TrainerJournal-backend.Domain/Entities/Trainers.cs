namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренера в системе.
/// Наследуется от <see cref="Entity{TKey}"/>, где в качестве TKey используется <see cref="Guid"/>.
/// </summary>
public class Trainers : Entity<Guid>
{
    /// <summary>
    /// Логин пользователя <see cref="UsersIdentity"/>, авторизовавшегося в системе.
    /// </summary>
    public required string UserName { get; init; }
    private UsersIdentity UserIdentity { get; set; }
}