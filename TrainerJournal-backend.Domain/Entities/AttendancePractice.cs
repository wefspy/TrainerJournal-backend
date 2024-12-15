using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность для учета посещаемости тренировок.
/// </summary>
public class AttendancePractice : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор тренировки <see cref="Practice"/>,
    /// для которой ведется учет посещаемости.
    /// </summary>
    public Guid PracticeId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор ученика <see cref="Student"/>,
    /// учет посещаемости которого ведется.
    /// </summary>
    public Guid StudentId { get; init; }
    
    /// <summary>
    /// Статус посещения занятия <see cref="AttendanceStatus"/>
    /// </summary>
    public AttendanceStatus Status { get; set; }
}