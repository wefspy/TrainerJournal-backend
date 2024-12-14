using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность описания платежа <see cref="Payments"/>.
/// </summary>
public class PaymentsInfo : Entity<Guid>
{
    /// <summary>
    /// Дата совершения платежа.
    /// </summary>
    public required DateOnly Date { get; set; }
    
    /// <summary>
    /// Сумма, на которую был совершен платеж
    /// </summary>
    public required double Amount { get; set; }
    
    /// <summary>
    /// Текущий статус платежа
    /// </summary>
    public required PaymentStatus Status { get; set; }
}