namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность описания ученика.
/// </summary>
public class StudentInfo : Entity<Guid>
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public Student Student { get; private set; }
    
    /// <summary>
    /// День рождения
    /// </summary>
    public DateOnly DateOfBirth { get; set; }
    
    /// <summary>
    /// Адрес проживания
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// Номер класса, в котором учится на данным момент
    /// </summary>
    public int Class { get; set; }
    
    /// <summary>
    /// Дата начала обучения в секции
    /// </summary>
    public DateOnly BeginningTraining { get; set; }
}