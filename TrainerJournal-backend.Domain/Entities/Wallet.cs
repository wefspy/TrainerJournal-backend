namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность кошелька
/// </summary>
public class Wallet : Entity<Guid>
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public Student Student { get; private set; }
    
    /// <summary>
    /// Ссылка на объекты внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<Payment> Payments { get; private set; }
    
    /// <summary>
    /// Баланс кошелька на текущий момент
    /// </summary>
    public double Balance { get; set; }
}