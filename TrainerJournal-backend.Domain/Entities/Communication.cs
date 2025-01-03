namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность для хранения способов связи.
/// </summary>
public class Communication(
    string PhoneNumber,
    string Email) : Entity
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public Contact Contact { get; private set; }

    /// <summary>
    /// Номер телефона для связи.
    /// </summary>
    public string? PhoneNumber { get; set; } = PhoneNumber;
    
    /// <summary>
    /// Адрес электронной почты для связи.
    /// </summary>
    public string? Email { get; set; } = Email;
}