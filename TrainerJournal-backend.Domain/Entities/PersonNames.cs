namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность, хранящую имена людей.
/// Наследуется от <see cref="Entity{TKey}"/>, где в качестве TKey используется <see cref="Guid"/>.
/// </summary>
public class PersonNames : Entity<Guid>
{
    /// <summary>
    /// Имя человека.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия человека.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Отчество человека.
    /// </summary>
    public string MiddleName { get; set; }
}