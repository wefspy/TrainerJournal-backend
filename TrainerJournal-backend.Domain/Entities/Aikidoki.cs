namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность айкидоки (практикующего айкидо) в системе.
/// </summary>
public class Aikidoki : Entity<Guid>
{
    /// <summary>
    /// Логин пользователя <see cref="UsersIdentity"/>, авторизовавшегося в системе
    /// </summary>
    public required string UserName { get; init; }
    private UsersIdentity UserIdentity { get; set; }

    /// <summary>
    /// Уровень кю (ранг).
    /// Представляет целочисленное значение, определяющее степень владения навыками айкидо.
    /// </summary>
    public int Kyu { get; set; }
}