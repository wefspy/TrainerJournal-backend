using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerJournal_backend.Application.Services.Practices.DTOs;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.Application.Services.Practices;

public class PracticesService(
    AppDbContext db)
{
    public async Task<ObjectResult> GetByTrainer(string userName)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var trainer = await db.Trainers
                .Include(t => t.UserIdentity)
                .ThenInclude(i => i.UserInfo)
                .ThenInclude(u => u.PersonName)
                .FirstOrDefaultAsync(t => t.UserName == userName);
            if (trainer == null)
            {
                return new NotFoundObjectResult("Trainer not found");
            }

            var trainerName = trainer.UserIdentity.UserInfo.PersonName;

            var groups = await db.Groups.Where(g => g.TrainerId == trainer.Id).ToListAsync();

            var calendarPractices = new List<CalendarPracticeDTO>();
            foreach (var group in groups)
            {
                var practices = await db.Practices.Where(p => p.GroupId == group.Id).ToListAsync();
                foreach (var practice in practices)
                {
                    var calendarPractice = new CalendarPracticeDTO(
                        practice.Id,
                        practice.Date,
                        practice.TimeStart,
                        practice.TimeEnd,
                        group.Name,
                        trainerName.FirstName,
                        trainerName.LastName,
                        trainerName.MiddleName);

                    calendarPractices.Add(calendarPractice);
                }
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult(calendarPractices);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при создании записи" });
        }
    }

    public async Task<ObjectResult> GetByStudent(string userName)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var student = await db.Students
                .Include(s => s.StudentGroups)
                .ThenInclude(s => s.Group)
                .FirstOrDefaultAsync(s => s.UserName == userName);
            if (student == null)
            {
                return new NotFoundObjectResult("Student not found");
            }

            var groups = student.StudentGroups.Select(sg => sg.Group).ToList();

            var calendarPractices = new List<CalendarPracticeDTO>();
            foreach (var group in groups)
            {
                var trainer = await db.Trainers
                    .Include(t => t.UserIdentity)
                    .ThenInclude(i => i.UserInfo)
                    .ThenInclude(u => u.PersonName)
                    .FirstOrDefaultAsync(t => t.Id == group.TrainerId);
                var trainerName = trainer.UserIdentity.UserInfo.PersonName;
                var practices = await db.Practices.Where(p => p.GroupId == group.Id).ToListAsync();
                foreach (var practice in practices)
                {
                    var calendarPractice = new CalendarPracticeDTO(
                        practice.Id,
                        practice.Date,
                        practice.TimeStart,
                        practice.TimeEnd,
                        group.Name,
                        trainerName.FirstName,
                        trainerName.LastName,
                        trainerName.MiddleName);

                    calendarPractices.Add(calendarPractice);
                }
            }

            return new OkObjectResult(calendarPractices);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при создании записи" });
        }
    }

    public async Task<ObjectResult> Create(CreatePracticeRequest request)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var group = await db.Groups.FindAsync(request.groupId);
            if (group == null)
            {
                return new BadRequestObjectResult("Группы не существует");
            }

            var practice = new Practice(request.groupId,
                request.date,
                request.timeStart,
                request.timeEnd,
                group.CostPractice);

            await db.Practices.AddAsync(practice);

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            var practiceDTO = new PracticeDTO(
                practice.Id,
                practice.Date,
                practice.TimeStart,
                practice.TimeEnd);

            return new OkObjectResult(practiceDTO);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при создании записи" });
        }
    }

    public async Task<ObjectResult> Duplicate(string userName, DuplicationDTO request)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var trainer = await db.Trainers
                .Include(t => t.Groups)
                .ThenInclude(g => g.Practices)
                .FirstOrDefaultAsync(t => t.UserName == userName);

            if (trainer == null)
            {
                return new NotFoundObjectResult("Trainer not found");
            }

            if (request.DateEndCopy < request.DateStartCopy || request.DateEndPaste < request.DateStartPaste)
            {
                return new BadRequestObjectResult(new { message = "Invalid date ranges" });
            }

            var practicesToCopy = trainer.Groups
                .SelectMany(g => g.Practices)
                .Where(p => p.Date >= request.DateStartCopy && p.Date <= request.DateEndCopy)
                .ToList();

            if (!practicesToCopy.Any())
            {
                return new NotFoundObjectResult("No practices found in the specified copy range");
            }

            int copyRangeDays = (request.DateEndCopy.Day - request.DateStartCopy.Day) + 1;
            int pasteRangeDays = (request.DateEndPaste.Day - request.DateStartPaste.Day) + 1;

            if (pasteRangeDays < copyRangeDays)
            {
                return new BadRequestObjectResult(new
                    { message = "Paste range must be equal to or larger than the copy range" });
            }

            int iterations = (int)Math.Ceiling((double)pasteRangeDays / copyRangeDays);

            foreach (var group in trainer.Groups)
            {
                var groupPractices = practicesToCopy.Where(p => p.GroupId == group.Id).ToList();

                for (int i = 0; i < iterations; i++)
                {
                    foreach (var practice in groupPractices)
                    {
                        var newDate = practice.Date
                            .AddDays(i * copyRangeDays + (request.DateStartPaste.Day - request.DateStartCopy.Day));

                        if (newDate > request.DateEndPaste)
                        {
                            break; // Выходим, если дата превышает предел диапазона вставки
                        }

                        var newPractice = new Practice(
                            groupId: group.Id,
                            date: newDate,
                            timeStart: practice.TimeStart,
                            timeEnd: practice.TimeEnd,
                            cost: practice.Cost
                        );

                        db.Practices.Add(newPractice);
                    }
                }
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult("Practices duplicated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при создании записи", details = ex.Message });
        }
    }


    public async Task<ObjectResult> Update(PracticeDTO request)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var practice = await db.Practices.FindAsync(request.practiceId);
            if (practice == null)
            {
                return new BadRequestObjectResult("Занятия не существует");
            }

            practice.Date = request.date;
            practice.TimeStart = request.timeStart;
            practice.TimeEnd = request.timeEnd;

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            var practiceDto = new PracticeDTO(practice.Id, practice.Date, practice.TimeStart, practice.TimeEnd);

            return new OkObjectResult(practiceDto);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при создании записи" });
        }
    }

    public async Task<ObjectResult> Delete(Guid practiceId)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var practice = await db.Practices.FindAsync(practiceId);
            if (practice == null)
            {
                return new BadRequestObjectResult("Занятия не существует");
            }

            var delEntity = db.Practices.Remove(practice).Entity;

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult(delEntity.Id);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при создании записи" });
        }
    }
}