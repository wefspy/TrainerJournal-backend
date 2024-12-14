namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность чека об оплате
/// </summary>
public class Receipts : Entity<Guid>
{
    /// <summary>
    /// Адрес, хранящий файл чека
    /// </summary>
    public required string Url { get; set; }
}