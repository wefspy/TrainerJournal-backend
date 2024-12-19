namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренировки
/// </summary>
public class Practice(
    Guid groupId,
    DateOnly date,
    TimeOnly timeStart,
    TimeOnly timeEnd,
    double cost) : Entity
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<AttendancePractice> AttendancePractices { get; private set; }

    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Group"/>,
    /// к которой относится тренировка
    /// </summary>
    public Guid GroupId { get; init; } = groupId;
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="GroupId"/>
    /// </summary>
    public Group Group { get; private set; }
    
    /// <summary>
    /// Дата проведения тренировки.
    /// </summary>
    public DateOnly Date { get; set; } = date;
    
    /// <summary>
    /// Время начала тренировки
    /// </summary>
    public TimeOnly TimeStart { get; set; } = timeStart;
    
    /// <summary>
    /// Время окончания тренировки
    /// </summary>
    public TimeOnly TimeEnd { get; set; } = timeEnd;
    
    /// <summary>
    /// Стоимость посещения тренировки.
    /// По умолчанию, назначается текущее значение из закрепленной группы <see cref="Entities.Group"/>
    /// </summary>
    public double Cost { get; set; } = cost;
}