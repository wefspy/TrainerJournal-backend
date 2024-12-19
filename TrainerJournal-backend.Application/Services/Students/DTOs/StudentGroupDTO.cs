using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Application.Services.Students.DTOs;

public record StudentGroupDTO(
    Guid StudentId,
    string UserName,
    string FirstName,
    string LastName,
    string MiddleName,
    int Kyu,
    DateOnly DateOfBirth,
    int Class,
    string Address,
    string PhoneNumber,
    string Email,
    Gender Gender,
    double WalletBalance,
    List<GroupDTO> Groups
);

public record GroupDTO(
    Guid GroupId,
    string GroupName
);