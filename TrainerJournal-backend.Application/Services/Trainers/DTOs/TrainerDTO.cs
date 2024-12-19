namespace TrainerJournal_backend.Application.Services.Trainers.DTOs;

public record TrainerDTO(
    Guid TrainerId,
    string FirstName,
    string LastName,
    string MiddleName
);