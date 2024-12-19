using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.Trainers;
using TrainerJournal_backend.Application.Services.Trainers.DTOs;
using TrainerJournal_backend.Domain.Constants;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/trainer")]
[Authorize(Roles = Roles.Trainer)]
public class TrainerController(
    TrainerService trainerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TrainerDTO>>> GetByStudentUserName(string userName)
    {
        return await trainerService.GetByStudentUserName(userName);
    }
    
    [HttpGet("info")]
    public async Task<ActionResult<TrainerInfo>> GetInfo(string userName)
    {
        return await trainerService.GetUserInfo(userName);
    }
    
    [HttpPut("info")]
    public async Task<ActionResult<TrainerInfo>> ChangeInfo(string userName, TrainerInfo request)
    {
        return await trainerService.ChangeUserInfo(userName, request);
    }
}