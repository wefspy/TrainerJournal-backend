namespace TrainerJournal_backend.Domain.Enums;

/// <summary>
/// Представляет статус платежа
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Одобрен
    /// </summary>
    Approved,
    
    /// <summary>
    /// Отколнен
    /// </summary>
    Rejected,
    
    /// <summary>
    /// На рассмотрении
    /// </summary>
    UnderConsideration,
}