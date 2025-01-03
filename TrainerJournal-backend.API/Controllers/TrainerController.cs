using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.Trainers;
using TrainerJournal_backend.Application.Services.Trainers.DTOs;
using TrainerJournal_backend.Domain.Constants;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/trainer")]
public class TrainerController(
    TrainerService trainerService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Student)]
    public async Task<ActionResult<List<TrainerDTO>>> GetByStudentUserName(string userName)
    {
        return await trainerService.GetByStudentUserName(userName);
    }
    
    [HttpGet("info")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<TrainerInfo>> GetInfo(string userName)
    {
        return await trainerService.GetUserInfo(userName);
    }
    
    [HttpPut("info")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<TrainerInfo>> ChangeInfo(string userName, TrainerInfo request)
    {
        return await trainerService.ChangeUserInfo(userName, request);
    }
}