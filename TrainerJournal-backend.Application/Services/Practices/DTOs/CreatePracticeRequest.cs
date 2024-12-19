namespace TrainerJournal_backend.Application.Services.Practices.DTOs;

public record CreatePracticeRequest(
    Guid groupId,
    DateOnly date,
    TimeOnly timeStart,
    TimeOnly timeEnd);