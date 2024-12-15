namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность ученика, посещающего тренировки.
/// </summary>
public class Students : Entity<Guid>
{
    /// <summary>
    /// Логин пользователя <see cref="UsersIdentity"/>, авторизовавшегося в системе.
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