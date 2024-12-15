namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренирующейся группы.
/// </summary>
public class Group : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор тренера <see cref="Trainer"/> группы.
    /// </summary>
    public Guid TrainerId { get; init; }
    
    /// <summary>
    /// Название группы.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Стоимость одной посещения одной тренировки группы.
    /// </summary>
    public double CostTraining { get; set; }
}