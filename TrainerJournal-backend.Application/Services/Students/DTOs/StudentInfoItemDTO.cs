using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Application.Services.Students.DTOs;

public record StudentInfoItemDTO(
    string FirstName,
    string LastName,
    string MiddleName,
    DateOnly DateOfBirth,
    int Kyu,
    int Class,
    string Address,
    string PhoneNumber,
    string Email,
    Gender Gender
);