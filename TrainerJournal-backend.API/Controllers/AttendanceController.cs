using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.Attendance;
using TrainerJournal_backend.Application.Services.Attendance.DTOs;
using TrainerJournal_backend.Domain.Constants;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/attendance")]
[Authorize(Roles = Roles.Trainer)]
public class AttendanceController(
    AttendanceService attendanceService)
{
    [HttpGet("practices/{id}")]
    public async Task<ActionResult<List<AttendanceStudentDTO>>> GeyByPractice(Guid id)
    {
        return await attendanceService.GetByPractice(id);
    }
    
    [HttpGet("groups/{id}")]
    public async Task<ActionResult<List<AttendanceStudentGroupDTO>>> GetByGroup(Guid id)
    {
        return await attendanceService.GetByGroup(id);
    }
    
    [HttpPut]
    public async Task<ActionResult> Update(List<AttendanceDTO> attendanceDtos)
    {
        return await attendanceService.Update(attendanceDtos);
    }
}