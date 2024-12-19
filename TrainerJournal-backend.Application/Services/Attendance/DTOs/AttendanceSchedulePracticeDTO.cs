namespace TrainerJournal_backend.Application.Services.Attendance.DTOs;

public record AttendanceSchedulePracticeDTO(
    Guid AttendanceId,
    DateOnly Date,
    bool Status
);