namespace TrainerJournal_backend.Application.Services.Groups.DTOs;

public record GroupResponse(
    Guid id,
    string name,
    double costPractice,
    int numberStudents
);