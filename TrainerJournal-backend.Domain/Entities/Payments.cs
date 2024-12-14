namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность платёжа
/// </summary>
public class Payments : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор тренера <see cref="Trainers"/>,
    /// которому будет направлен платеж на рассмотрение.
    /// </summary>   
    public required Guid TrainerId { get; init; }
    private Trainers Trainer { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор чека <see cref="Receipts"/>,
    /// который подтверждает факт совершения платежа.
    /// </summary>
    public required Guid ReceiptId { get; init; }
    private Receipts Receipt { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор кошелька <see cref="Wallets"/>,
    /// к которому будет привязан платеж
    /// </summary>
    public required Guid WalletId { get; init; }
    private Wallets Wallet { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор описания совершаемого платежа <see cref="PaymentsInfo"/>
    /// </summary>
    public required Guid PaymentInfoId { get; init; }
    private PaymentsInfo PaymentInfo { get; set; }
}