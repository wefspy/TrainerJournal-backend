namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренирующейся группы.
/// </summary>
public class Groups : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор тренера <see cref="Trainers"/> группы.
    /// </summary>
    public Guid TrainerId { get; init; }
    /// <summary>
    /// Объект тренера <see cref="Trainers"/>.
    /// </summary>
    private Trainers Trainer { get; set; }
    
    /// <summary>
    /// Название группы.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Стоимость одной посещения одной тренировки группы.
    /// </summary>
    public double CostTraining { get; set; }
}