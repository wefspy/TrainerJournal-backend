namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность связку между учеником и его контактами
/// </summary>
public class StudentContact : Entity<Guid>
{
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Student"/>,
    /// владеющего контактом
    /// </summary>
    public Guid StudentId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="StudentId"/>
    /// </summary>
    public Student Student { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Contact"/>,
    /// относящегося к ученику.
    /// </summary>
    public Guid ContactId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="ContactId"/>
    /// </summary>
    public Contact Contact { get; private set; }
}