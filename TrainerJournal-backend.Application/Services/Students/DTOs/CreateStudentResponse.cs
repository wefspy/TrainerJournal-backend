namespace TrainerJournal_backend.Application.Services.Students.DTOs;

public record CreateStudentResponse(
    string UserName,
    string Password
);