using Microsoft.AspNetCore.Http;

namespace TrainerJournal_backend.Application.Services.Payments.DTOs;

public record CreatePaymentDTO(
    Guid TrainerId,
    double Amount,
    IFormFile File);