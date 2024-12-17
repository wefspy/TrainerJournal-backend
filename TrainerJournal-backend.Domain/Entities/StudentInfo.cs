namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность описания ученика.
/// </summary>
public class StudentInfo(
    DateOnly dateOfBirth,
    string address,
    int Class,
    DateOnly beginningTraining) : Entity
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public Student Student { get; private set; }

    /// <summary>
    /// День рождения
    /// </summary>
    public DateOnly DateOfBirth { get; set; } = dateOfBirth;

    /// <summary>
    /// Адрес проживания
    /// </summary>
    public string Address { get; set; } = address;

    /// <summary>
    /// Номер класса, в котором учится на данным момент
    /// </summary>
    public int Class { get; set; } = Class;

    /// <summary>
    /// Дата начала обучения в секции
    /// </summary>
    public DateOnly BeginningTraining { get; set; } = beginningTraining;
}