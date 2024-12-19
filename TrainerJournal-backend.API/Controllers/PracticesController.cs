using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.Practices;
using TrainerJournal_backend.Application.Services.Practices.DTOs;
using TrainerJournal_backend.Domain.Constants;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/practices")]
[Authorize(Roles = Roles.Trainer)]
public class PracticesController(
    PracticesService practicesService)
{
    [HttpGet("trainer/{userName}")]
    public async Task<ActionResult<List<CalendarPracticeDTO>>> GetByTrainer(string userName)
    {
        return await practicesService.GetByTrainer(userName);
    }

    [HttpGet("student/{userName}")]
    public async Task<ActionResult<List<CalendarPracticeDTO>>> GetByStudent(string userName)
    {
        return await practicesService.GetByStudent(userName);
    }
    
    [HttpPost]
    public async Task<ActionResult<PracticeDTO>> Create(CreatePracticeRequest request)
    {
        return await practicesService.Create(request);
    }
    
    [HttpPut]
    public async Task<ActionResult<PracticeDTO>> Update(PracticeDTO request)
    {
        return await practicesService.Update(request);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<PracticeDTO>> Delete(Guid id)
    {
        return await practicesService.Delete(id);
    }
}