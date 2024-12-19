using Microsoft.AspNetCore.Http;

namespace TrainerJournal_backend.Application.Services.Payments.DTOs;

public record CreatePaymentDTO(
    string TrainerUserName,
    double Amount,
    IFormFile File);