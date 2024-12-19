namespace TrainerJournal_backend.Application.Services.Practices.DTOs;

public record CalendarPracticeDTO(
    Guid practiceId,
    DateOnly date,
    TimeOnly timeStart,
    TimeOnly timeEnd,
    string groupNamem,
    string trainerFirstName,
    string trainerLastName,
    string trainerMiddleName);