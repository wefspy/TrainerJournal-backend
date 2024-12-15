namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренировки
/// </summary>
public class Practice : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор группы <see cref="Group"/>,
    /// к которой относится тренировка
    /// </summary>
    public Guid GroupId { get; init; }
    
    /// <summary>
    /// Дата и время проведения тренировки.
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Стоимость посещения тренировки.
    /// По умолчанию, назначается текущее значение из закрепленной группы <see cref="Group"/>
    /// </summary>
    public double Cost { get; set; }
}