using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность для учета посещаемости тренировок.
/// </summary>
public class AttendancePractice(
    Guid practiceId,
    Guid studentId,
    bool isAttend) : Entity
{
    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Practice"/>,
    /// для которой ведется учет посещаемости.
    /// </summary>
    public Guid PracticeId { get; init; } = practiceId;
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="PracticeId"/>
    /// </summary>
    public Practice Practice { get; private set; }

    /// <summary>
    /// Внешний ключ для связи с <see cref="Entities.Student"/>,
    /// учет посещаемости которого ведется.
    /// </summary>
    public Guid StudentId { get; init; } = studentId;
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="StudentId"/>
    /// </summary>
    public Student Student { get; private set; }

    /// <summary>
    /// Статус посещения занятия <see cref="AttendanceStatus"/>
    /// </summary>
    public bool IsAttend { get; set; } = isAttend;
}