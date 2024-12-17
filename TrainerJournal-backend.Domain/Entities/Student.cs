namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность ученика, посещающего тренировки.
/// </summary>
public class Student(
    string userName,
    Guid walletId,
    Guid studentInfoId) : Entity
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<StudentGroup> StudentGroups { get; private set; }
    
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<AttendancePractice> AttendancePractices { get; private set; }
    
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Entities.Entity.Id"/>
    /// </summary>
    public List<StudentContact> StudentContacts { get; private set; }

    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.UserIdentity"/>
    /// </summary>
    public string UserName { get; init; } = userName;
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="UserName"/>
    /// </summary>
    public UserIdentity UserIdentity { get; private set; }

    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Wallet"/>
    /// </summary>
    public Guid WalletId { get; init; } = walletId;
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="WalletId"/>
    /// </summary>
    public Wallet Wallet { get; private set; }

    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.StudentInfo"/>
    /// </summary>
    public Guid StudentInfoId { get; init; } = studentInfoId;
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="StudentInfoId"/>
    /// </summary>
    public StudentInfo StudentInfo { get; private set; }
}