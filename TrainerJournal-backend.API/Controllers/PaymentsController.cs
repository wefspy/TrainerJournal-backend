using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.Payments;
using TrainerJournal_backend.Application.Services.Payments.DTOs;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController(
    PaymentsService paymentsService) : ControllerBase
{
    [HttpGet("trainers/{userName}")]
    public async Task<IActionResult> GetByTrainerUserName(string userName)
    {
        return await paymentsService.GetByTrainerUserName(userName);
    }
    
    [HttpGet("students/{userName}")]
    public async Task<IActionResult> GetByStudentUserName(string userName)
    {
        return await paymentsService.GetByStudentUserName(userName);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(string studentUserName, CreatePaymentDTO paymentDTO)
    {
        return await paymentsService.Create(studentUserName, paymentDTO);
    }
}
