namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренировки
/// </summary>
public class Practice : Entity<Guid>
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<AttendancePractice> AttendancePractices { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Group"/>,
    /// к которой относится тренировка
    /// </summary>
    public Guid GroupId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="GroupId"/>
    /// </summary>
    public Group Group { get; private set; }
    
    /// <summary>
    /// Дата и время проведения тренировки.
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Стоимость посещения тренировки.
    /// По умолчанию, назначается текущее значение из закрепленной группы <see cref="Entities.Group"/>
    /// </summary>
    public double Cost { get; set; }
}