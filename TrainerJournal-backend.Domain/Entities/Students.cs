namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность ученика, посещающего тренировки.
/// </summary>
public class Students : Entity<Guid>
{
    /// <summary>
    /// Логин пользователя <see cref="UsersIdentity"/>, авторизовавшегося в системе.
    /// </summary>
    public required string UserName { get; init; }
    private UsersIdentity UserIdentity { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор кошелька ученика для оплаты услуг.
    /// </summary>
    public required Guid WalletId { get; init; }
    private Wallets Wallet { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор 
    /// </summary>
    public required Guid StudentInfoId { get; init; }
    private StudentsInfo StudentInfo { get; set; }
}