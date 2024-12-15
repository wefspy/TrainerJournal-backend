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
    public Guid TrainingId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор ученика <see cref="Students"/>,
    /// учет посещаемости которого ведется.
    /// </summary>
    public Guid StudentId { get; init; }
    
    /// <summary>
    /// Статус посещения занятия <see cref="AttendanceStatus"/>
    /// </summary>
    public AttendanceStatus Status { get; set; }
}