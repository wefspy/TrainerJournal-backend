namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность кошелька
/// </summary>
public class Wallets : Entity<Guid>
{
    /// <summary>
    /// Баланс кошелька на текущий момент
    /// </summary>
    public double Balance { get; set; }
}