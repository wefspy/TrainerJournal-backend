namespace TrainerJournal_backend.Application.Services.Students.DTOs;

public record StudentShortInfoItemDTO(
    string UserName,
    string FirstName,
    string LastName,
    string MiddleName,
    string GroupName,
    double WalletBalance,
    int Kyu
);