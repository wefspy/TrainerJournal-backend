using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerJournal_backend.Application.Services.Attendance.DTOs;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Domain.Enums;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.Application.Services.Attendance;

public class AttendanceService(
    AppDbContext db)
{
    public async Task<ObjectResult> GetByPractice(Guid practiceId)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        
        try
        {
            var practice = await db.Practices
                .Include(p => p.Group)
                .ThenInclude(g => g.StudentsGroup)
                .ThenInclude(g => g.Student)
                .ThenInclude(s => s.UserIdentity)
                .ThenInclude(i => i.UserInfo)
                .ThenInclude(i => i.PersonName)
                .FirstOrDefaultAsync(p => p.Id == practiceId);
        
            if (practice == null)
            {
                return new NotFoundObjectResult(new { message = "Практика не найдена" });
            }
            
            var students = practice.Group.StudentsGroup.Select(sg => sg.Student).ToList();
        
            var attendanceStudents = new List<AttendanceStudentDTO>();
            foreach (var student in students)
            {
                var attendancePractice = await db.AttendancePractices.FirstOrDefaultAsync(a => a.StudentId == student.Id && a.PracticeId == practiceId);
                if (attendancePractice == null)
                {
                    attendancePractice = new AttendancePractice(practiceId, student.Id, false);
                    await db.AttendancePractices.AddAsync(attendancePractice);
                }
            
                var personName = student.UserIdentity.UserInfo.PersonName;

                var attendanceStudent = new AttendanceStudentDTO(
                    attendancePractice.Id,
                    personName.FirstName,
                    personName.LastName,
                    personName.MiddleName,
                    attendancePractice.Status
                );
            
                attendanceStudents.Add(attendanceStudent);
            }
            
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return new OkObjectResult(attendanceStudents);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка обновлении записи" });
        }
    }
    
    public async Task<ObjectResult> GetByGroup(Guid groupId)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var group = await db.Groups
                .Include(g => g.StudentsGroup)
                .ThenInclude(g => g.Student)
                .ThenInclude(s => s.UserIdentity)
                .ThenInclude(i => i.UserInfo)
                .ThenInclude(i => i.PersonName)
                .Include(g => g.Practices)
                .ThenInclude(p => p.AttendancePractices)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
            {
                return new NotFoundObjectResult(new { message = "Группа не найдена" });
            }

            var practices = group.Practices;
            var students = group.StudentsGroup.Select(sg => sg.Student).ToList();
            
            var attendanceStudentsGroup = new List<AttendanceStudentGroupDTO>();
            foreach (var student in students)
            {
                var personName = student.UserIdentity.UserInfo.PersonName;
                
                var attendancePracticesDTOs = new List<AttendanceSchedulePracticeDTO>();
                foreach (var practice in practices)
                {
                    var attendancePractice = practice.AttendancePractices.FirstOrDefault(a =>
                        a.StudentId == student.Id && a.PracticeId == practice.Id);

                    if (attendancePractice != null)
                    {
                        var attendancePracticesDTO = new AttendanceSchedulePracticeDTO(
                            attendancePractice.Id, 
                            practice.Date, 
                            attendancePractice.Status);
                        
                        attendancePracticesDTOs.Add(attendancePracticesDTO);
                    }
                }
                
                var attendanceStudentGroupDto = new AttendanceStudentGroupDTO(
                    personName.FirstName, 
                    personName.LastName, 
                    personName.MiddleName, 
                    attendancePracticesDTOs);
                attendanceStudentsGroup.Add(attendanceStudentGroupDto);
            }
            
            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult(attendanceStudentsGroup);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            return new BadRequestObjectResult(new
            {
                message = "Ошибка получении записи",
                error = ex.Message
            });
        }
    }
    
    public async Task<ObjectResult> Update(List<AttendanceDTO> attendanceDtos)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var attendanceIds = attendanceDtos.Select(a => a.Id).ToList();
            
            var attendances = await db.AttendancePractices
                .Where(a => attendanceIds.Contains(a.Id))
                .ToListAsync();
            
            var missingIds = attendanceIds.Except(attendances.Select(a => a.Id)).ToList();
            if (missingIds.Any())
            {
                return new NotFoundObjectResult(new
                {
                    message = "Некоторые записи о посещаемости не найдены",
                    missingIds
                });
            }
            
            foreach (var attendance in attendances)
            {
                var dto = attendanceDtos.First(a => a.Id == attendance.Id);
                attendance.Status = dto.Status;
            }
            
            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult(new { message = "Записи успешно обновлены" });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            return new BadRequestObjectResult(new
            {
                message = "Ошибка обновления записей",
                error = ex.Message
            });
        }
    }
}