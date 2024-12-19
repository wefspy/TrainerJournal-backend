namespace TrainerJournal_backend.Application.Services.Trainers.DTOs;

public record TrainerDTO(
    string UserName,
    string FirstName,
    string LastName,
    string MiddleName
);