namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность для хранения контактной информации.
/// Содержит данные о человеке и методах связи с ним.
/// </summary>
public class Contact(
    Guid personNameId,
    Guid communicationId,
    string relation) : Entity<Guid>
{
    /// <summary>
    /// Ссылка на объекты внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<StudentContact> StudentContacts { get; private set; }

    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.PersonName"/>.
    /// </summary>
    public Guid PersonNameId { get; init; } = personNameId;
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="PersonNameId"/>
    /// </summary>
    public PersonName PersonName { get; private set; }

    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Communication"/>.
    /// </summary>
    public Guid CommunicationId { get; init; } = communicationId;
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="CommunicationId"/>
    /// </summary>
    public Communication Communication { get; private set; }

    /// <summary>
    /// Описание кем приходится контакт ученику <see cref="Student"/>.
    /// </summary>
    public string Relation { get; set; } = relation;
}