using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.DTOs;
using TrainerJournal_backend.Application.Services.Groups;
using TrainerJournal_backend.Application.Services.Groups.DTOs;
using TrainerJournal_backend.Domain.Constants;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/groups")]
[Authorize(Roles = Roles.Trainer)]
public class GroupsController(
    GroupsService groupsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GroupResponse>>> GetGroups(string userName)
    {
        return await groupsService.GetGroups(userName);
    }
    
    [HttpPost]
    public async Task<ActionResult<GroupResponse>> GetGroups(string userName, GroupRequest request)
    {
        return await groupsService.CreateGroup(userName, request);
    }
    
    [HttpPut("{groupId}")]
    public async Task<ActionResult<GroupResponse>> GetGroups(Guid groupId, GroupRequest request)
    {
        return await groupsService.ChangeGroup(groupId, request);
    }
    
    [HttpPost("{groupId}")]
    public async Task<ActionResult<GroupResponse>> GetGroups(Guid groupId, Guid studentId)
    {
        return await groupsService.AddStudent(groupId, studentId);
    }
    
    [HttpDelete("{groupId}")]
    public async Task<ActionResult<DeletedEntity>> GetGroups(Guid groupId)
    {
        return await groupsService.DeleteGroup(groupId);
    }
}