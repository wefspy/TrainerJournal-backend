namespace TrainerJournal_backend.Application.Services.Attendance.DTOs;

public record AttendanceStudentGroupDTO(
    string FirstName,
    string LastName,
    string MiddleName,
    List<AttendanceSchedulePracticeDTO> AttendancePractices
);