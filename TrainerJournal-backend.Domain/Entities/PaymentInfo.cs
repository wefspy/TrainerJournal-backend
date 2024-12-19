using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность описания платежа <see cref="Payment"/>.
/// </summary>
public class PaymentInfo(
    DateOnly date,
    double amount,
    PaymentStatus status) : Entity
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public Payment Payment { get; private set; }

    /// <summary>
    /// Дата совершения платежа.
    /// </summary>
    public DateOnly Date { get; set; } = date;
    
    /// <summary>
    /// Сумма, на которую был совершен платеж
    /// </summary>
    public double Amount { get; set; } = amount;
    
    /// <summary>
    /// Текущий статус платежа
    /// </summary>
    public PaymentStatus Status { get; set; } = status;
}