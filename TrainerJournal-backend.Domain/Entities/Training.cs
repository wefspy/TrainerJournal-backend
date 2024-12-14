namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренировки
/// </summary>
public class Training : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор группы <see cref="Groups"/>,
    /// к которой относится тренировка
    /// </summary>
    public Guid GroupId { get; init; }
    private Groups Group { get; set; }
    
    /// <summary>
    /// Дата и время проведения тренировки.
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Стоимость посещения тренировки.
    /// По умолчанию, назначается текущее значение из закрепленной группы <see cref="Groups"/>
    /// </summary>
    public double Cost { get; set; }
}