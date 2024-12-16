namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность связку между группами и учениками
/// </summary>
public class StudentGroup : Entity<Guid>
{
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Group"/>,
    /// к которой принадлежит ученик.
    /// </summary>
    public Guid GroupId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="GroupId"/>
    /// </summary>
    public Group Group { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Student"/>.
    /// </summary>
    public Guid StudentId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="StudentId"/>
    /// </summary>
    public Student Student { get; private set; }
}