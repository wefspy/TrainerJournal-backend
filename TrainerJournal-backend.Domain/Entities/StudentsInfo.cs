namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность описания ученика.
/// </summary>
public class StudentsInfo : Entity<Guid>
{
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