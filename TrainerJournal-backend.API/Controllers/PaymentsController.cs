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
    public async Task<ActionResult<List<PaymentTrainerItemDTO>>> GetByTrainerUserName(string userName)
    {
        return await paymentsService.GetByTrainerUserName(userName);
    }
    
    [HttpGet("students/{userName}")]
    public async Task<ActionResult<List<PaymentStudentItemDTO>>> GetByStudentUserName(string userName)
    {
        return await paymentsService.GetByStudentUserName(userName);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(string studentUserName, [FromForm] CreatePaymentDTO paymentDTO)
    {
        return await paymentsService.Create(studentUserName, paymentDTO, Request);
    }
}
