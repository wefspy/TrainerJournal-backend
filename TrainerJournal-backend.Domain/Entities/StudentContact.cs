namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность связку между учеником и его контактами
/// </summary>
public class StudentContact : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор ученика <see cref="Student"/>,
    /// владеющего контактом
    /// </summary>
    public Guid StudentId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор контакта <see cref="Contact"/>,
    /// относящегося к ученику.
    /// </summary>
    public Guid ContactId { get; init; }
}