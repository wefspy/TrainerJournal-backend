using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Application.Services.Payments.DTOs;

public record PaymentStudentItemDTO(
    DateOnly Date,
    string FirstName,
    string LastName,
    string MiddleName,
    double Amount,
    PaymentStatus Status,
    string ReceiptUrl
);