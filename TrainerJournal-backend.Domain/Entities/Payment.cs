namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность платёжа
/// </summary>
public class Payment : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор тренера <see cref="Trainer"/>,
    /// которому будет направлен платеж на рассмотрение.
    /// </summary>   
    public Guid TrainerId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор чека <see cref="Receipt"/>,
    /// который подтверждает факт совершения платежа.
    /// </summary>
    public Guid ReceiptId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор кошелька <see cref="Wallet"/>,
    /// к которому будет привязан платеж
    /// </summary>
    public Guid WalletId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор описания совершаемого платежа <see cref="PaymentInfo"/>
    /// </summary>
    public Guid PaymentInfoId { get; init; }
}