namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность для хранения контактной информации.
/// Содержит данные о человеке и методах связи с ним.
/// </summary>
public class Contacts : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор имени <see cref="PeopleNames"/> контакта.
    /// </summary>
    public Guid PersonNameId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор методов связи <see cref="CommunicationMethods"/> с контактом.
    /// </summary>
    public Guid CommunicationMethodsId { get; init; }
    
    /// <summary>
    /// Описание кем приходится контакт ученику <see cref="Students"/>.
    /// </summary>
    public string Relation { get; set; }
}