namespace TrainerJournal_backend.Application.Services.Trainers.DTOs;

public record TrainerInfo(
    string UserName,
    string FirstName,
    string LastName,
    string MiddleName,
    int Kyu,
    string PhoneNumber,
    string Email
);