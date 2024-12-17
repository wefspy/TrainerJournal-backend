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
    public async Task<ActionResult<CreateStudentResponse>> GetStudents(string trainerUserName)
    {
        return await studentsService.GetStudents(trainerUserName);
    }
    
    [HttpGet("{studentUserName}")]
    public async Task<ActionResult<CreateStudentResponse>> GetUserInfo(string studentUserName)
    {
        return await studentsService.GetUserInfo(studentUserName);
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<CreateStudentResponse>> Create(CreateStudentRequest request)
    {
        return await studentsService.CreateStudent(request);
    }
    
    [HttpPut]
    public async Task<ActionResult<CreateStudentResponse>> ChangeUserInfo(string userName, StudentInfoItemDTO request)
    {
        return await studentsService.ChangeStudentInfo(userName, request);
    }
    
    [HttpGet("contacts/{studentUserName}")]
    public async Task<ActionResult<CreateStudentResponse>> GetContacts(string studentUserName)
    {
        return await studentsService.GetContacts(studentUserName);
    }
    
    [HttpPut("contacts")]
    public async Task<ActionResult<CreateStudentResponse>> ChangeContacts(List<ContactDTO> contacts)
    {
        return await studentsService.ChangeContacts(contacts);
    }
}