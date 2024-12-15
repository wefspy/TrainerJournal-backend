namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность связку между группами и учениками
/// </summary>
public class StudentGroup : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор группы <see cref="Group"/>,
    /// к которой принадлежит ученик.
    /// </summary>
    public Guid GroupId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор студента <see cref="Student"/>.
    /// </summary>
    public Guid StudentId { get; init; }
}