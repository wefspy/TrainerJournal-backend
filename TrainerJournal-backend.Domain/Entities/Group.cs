namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность тренирующейся группы.
/// </summary>
public class Group : Entity<Guid>
{
    /// <summary>
    /// Ссылка на объекты внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<StudentGroup> StudentsGroup { get; private set; }
    
    /// <summary>
    /// Ссылка на объекты внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<Practice> Practices { get; private set; }
    
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Trainer"/> группы.
    /// </summary>
    public Guid TrainerId { get; init; }
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="TrainerId"/>
    /// </summary>
    public Trainer Trainer { get; private set; }
    
    /// <summary>
    /// Название группы.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Стоимость одной посещения одной тренировки группы.
    /// </summary>
    public double CostTraining { get; set; }
}