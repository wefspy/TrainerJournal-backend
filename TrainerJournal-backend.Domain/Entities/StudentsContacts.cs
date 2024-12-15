namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность связку между учеником и его контактами
/// </summary>
public class StudentsContacts : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор ученика <see cref="Students"/>,
    /// владеющего контактом
    /// </summary>
    public Guid StudentId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор контакта <see cref="Contacts"/>,
    /// относящегося к ученику.
    /// </summary>
    public Guid ContactId { get; init; }
}