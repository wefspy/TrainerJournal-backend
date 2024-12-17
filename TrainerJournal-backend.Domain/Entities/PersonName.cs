namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность, хранящую имя человека.
/// </summary>
public class PersonName(
    string firstName,
    string lastName,
    string middleName) : Entity
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public UserInfo UserInfo { get; private set; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public Contact Contact { get; private set; }

    /// <summary>
    /// Имя человека.
    /// </summary>
    public string FirstName { get; set; } = firstName;

    /// <summary>
    /// Фамилия человека.
    /// </summary>
    public string LastName { get; set; } = lastName;

    /// <summary>
    /// Отчество человека.
    /// </summary>
    public string MiddleName { get; set; } = middleName;
}