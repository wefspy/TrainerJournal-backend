using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет сущность для учета посещаемости тренировок.
/// </summary>
public class AttendanceTraining : Entity<Guid>
{
    /// <summary>
    /// Уникальный идентификатор тренировки <see cref="Training"/>,
    /// для которой ведется учет посещаемости.
    /// </summary>
    public required Guid TrainingId { get; init; }
    private Training Training { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор ученика <see cref="Students"/>,
    /// учет посещаемости которого ведется.
    /// </summary>
    public required Guid StudentId { get; init; }
    private Students Student { get; set; }
    
    /// <summary>
    /// Статус посещения занятия <see cref="AttendanceStatus"/>
    /// </summary>
    public required AttendanceStatus Status { get; set; }
}