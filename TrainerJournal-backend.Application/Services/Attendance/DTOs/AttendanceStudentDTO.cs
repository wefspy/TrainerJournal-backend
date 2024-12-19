namespace TrainerJournal_backend.Application.Services.Attendance.DTOs;

public record AttendanceStudentDTO(
    Guid AttendanceId,
    string FirstName,
    string LastName,
    string MiddleName,
    int Kyu,
    bool Status
);