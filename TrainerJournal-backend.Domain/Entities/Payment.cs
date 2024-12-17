namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность платёжа
/// </summary>
public class Payment : Entity
{
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Trainer"/>,
    /// которому будет направлен платеж на рассмотрение.
    /// </summary>   
    public Guid TrainerId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="TrainerId"/>
    /// </summary>
    public Trainer Trainer { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Receipt"/>,
    /// который подтверждает факт совершения платежа.
    /// </summary>
    public Guid ReceiptId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="ReceiptId"/>
    /// </summary>
    public Receipt Receipt { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Wallet"/>,
    /// к которому будет привязан платеж
    /// </summary>
    public Guid WalletId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="WalletId"/>
    /// </summary>
    public Wallet Wallet { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.PaymentInfo"/>
    /// </summary>
    public Guid PaymentInfoId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="PaymentInfoId"/>
    /// </summary>
    public PaymentInfo PaymentInfo { get; private set; }
}