namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность чека об оплате
/// </summary>
public class Receipt : Entity<Guid>
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public Payment Payment { get; private set; }
    
    /// <summary>
    /// Адрес, хранящий файл чека
    /// </summary>
    public string Url { get; set; }
}