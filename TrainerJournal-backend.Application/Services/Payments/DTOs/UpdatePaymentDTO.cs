using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Application.Services.Payments.DTOs;

public record UpdatePaymentDTO(
  PaymentStatus Status);