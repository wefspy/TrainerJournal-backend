namespace TrainerJournal_backend.Application.Services.Practices.DTOs;

public record PracticeDTO(
    Guid practiceId,
    DateOnly date,
    TimeOnly timeStart,
    TimeOnly timeEnd);