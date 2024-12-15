namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность айкидоки (практикующего айкидо) в системе.
/// </summary>
public class Aikidoka : Entity<Guid>
{
    /// <summary>
    /// Логин пользователя <see cref="UserIdentity"/>, авторизовавшегося в системе
    /// </summary>
    public string UserName { get; init; }

    /// <summary>
    /// Уровень кю (ранг).
    /// Представляет целочисленное значение, определяющее степень владения навыками айкидо.
    /// </summary>
    public int Kyu { get; set; }
}