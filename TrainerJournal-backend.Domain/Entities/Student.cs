namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность ученика, посещающего тренировки.
/// </summary>
public class Student : Entity<Guid>
{
    /// <summary>
    /// Логин пользователя <see cref="UserIdentity"/>, авторизовавшегося в системе.
    /// </summary>
    public string UserName { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор кошелька ученика для оплаты услуг.
    /// </summary>
    public Guid WalletId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор 
    /// </summary>
    public Guid StudentInfoId { get; init; }
}