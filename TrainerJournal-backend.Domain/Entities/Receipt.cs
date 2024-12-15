namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность чека об оплате
/// </summary>
public class Receipt : Entity<Guid>
{
    /// <summary>
    /// Адрес, хранящий файл чека
    /// </summary>
    public string Url { get; set; }
}