namespace TrainerJournal_backend.Application.Services.Students.DTOs;

public record ContactItemDTO(
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    string Email,
    string Relation
);