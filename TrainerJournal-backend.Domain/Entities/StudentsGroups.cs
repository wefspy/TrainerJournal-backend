namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность связку между группами и учениками
/// </summary>
public class StudentsGroups : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор группы <see cref="Groups"/>,
    /// к которой принадлежит ученик.
    /// </summary>
    public Guid GroupId { get; init; }
    private Groups Group { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор студента <see cref="Students"/>.
    /// </summary>
    public Guid StudentId { get; init; }
    private Students Student { get; set; }
}