using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.DTOs;
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
        return await paymentsService.Create(studentUserName, paymentDTO);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdatePaymentDTO request)
    {
        return await paymentsService.Update(id, request);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedEntity>> Delete(Guid id)
    {
        return await paymentsService.Delete(id);
    }
}
