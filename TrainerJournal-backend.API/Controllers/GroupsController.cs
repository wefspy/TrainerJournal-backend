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
    
    [HttpPut("search")]
    public async Task<ActionResult<List<GroupResponse>>> GetGroupsBySearch(string userName, GroupSearchDTO search)
    {
        return await groupsService.GetGroupsBySearch(userName, search);
    }
    
    [HttpPost]
    public async Task<ActionResult<GroupResponse>> Create(string userName, GroupRequest request)
    {
        return await groupsService.CreateGroup(userName, request);
    }
    
    [HttpPut("{groupId}")]
    public async Task<ActionResult> Update(Guid groupId, GroupRequest request)
    {
        return await groupsService.ChangeGroup(groupId, request);
    }
    
    [HttpPost("{groupId}")]
    public async Task<ActionResult> AddStudent(Guid groupId, Guid studentId)
    {
        return await groupsService.AddStudent(groupId, studentId);
    }
    
    [HttpDelete("{groupId}")]
    public async Task<ActionResult<DeletedEntity>> Delete(Guid groupId)
    {
        return await groupsService.DeleteGroup(groupId);
    }
}