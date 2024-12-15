using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность описания платежа <see cref="Payment"/>.
/// </summary>
public class PaymentInfo : Entity<Guid>
{
    /// <summary>
    /// Дата совершения платежа.
    /// </summary>
    public DateOnly Date { get; set; }
    
    /// <summary>
    /// Сумма, на которую был совершен платеж
    /// </summary>
    public double Amount { get; set; }
    
    /// <summary>
    /// Текущий статус платежа
    /// </summary>
    public PaymentStatus Status { get; set; }
}