using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Application.Services.Payments.DTOs;

public record PaymentTrainerItemDTO(
    string FirstName,
    string LastName,
    string MiddleName,
    List<String> GroupsNames,
    double Amount,
    PaymentStatus Status,
    string ReceiptUrl
);