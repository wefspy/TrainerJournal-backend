using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.Students;
using TrainerJournal_backend.Application.Services.Students.DTOs;
using TrainerJournal_backend.Domain.Constants;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/students")]
[Authorize(Roles = $"{Roles.Trainer}, {Roles.Student}")]
public class StudentController(
    StudentsService studentsService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<List<StudentShortInfoItemDTO>>> GetStudents(string trainerUserName)
    {
        return await studentsService.GetStudents(trainerUserName);
    }
    
    [HttpGet("{userName}")]
    public async Task<ActionResult<StudentInfoItemDTO>> GetUserInfo(string userName)
    {
        return await studentsService.GetUserInfo(userName);
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<CreateStudentResponse>> Create(CreateStudentRequest request)
    {
        return await studentsService.CreateStudent(request);
    }
    
    [HttpPut("{userName}")]
    public async Task<ActionResult<StudentInfoItemDTO>> ChangeUserInfo(string userName, StudentInfoItemDTO request)
    {
        return await studentsService.ChangeStudentInfo(userName, request);
    }
    
    [HttpGet("contacts/{userName}")]
    public async Task<ActionResult<List<ContactDTO>>> GetContacts(string userName)
    {
        return await studentsService.GetContacts(userName);
    }
    
    [HttpPut("contacts")]
    public async Task<ActionResult<List<ContactDTO>>> ChangeContacts(List<ContactDTO> contacts)
    {
        return await studentsService.ChangeContacts(contacts);
    }
}