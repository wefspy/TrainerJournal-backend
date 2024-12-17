using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerJournal_backend.Application.Services.Groups.DTOs;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.Application.Services.Groups;

public class GroupsService(
    AppDbContext db)
{
    public async Task<ObjectResult> GetGroups(string userName)
    {
        var groups = await db.Groups
            .Where(g => g.Trainer.UserName == userName)
            .Include(x => x.StudentsGroup)
            .Select(x => new GroupResponse(x.Id, x.Name, x.CostPractice, x.StudentsGroup.Count))
            .ToListAsync();
        
        return new OkObjectResult(groups);
    }

    public async Task<ObjectResult> CreateGroup(string userName, GroupRequest request)
    {
        var trainer = await db.Trainers.FirstOrDefaultAsync(t => t.UserName == userName);
        if (trainer == null)
        {
            return new NotFoundObjectResult(new { message = "Ошибка. Пользователь не добавлен в группу тренеры" });
        }
        await using var transaction = await db.Database.BeginTransactionAsync();
        
        try
        {
            var group = new Group(trainer.Id, request.name, request.costPractice);
            var result = await db.Groups.AddAsync(group);
            
            
            var entity = result.Entity;
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            
            var response = new GroupResponse(entity.Id, entity.Name, entity.CostPractice, 0);
            
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при создании группы" });
        }
    }
    
    public async Task<ObjectResult> ChangeGroup(Guid groupId, GroupRequest request)
    {
        var group = await db.Groups.FindAsync(groupId);
        
        await using var transaction = await db.Database.BeginTransactionAsync();
        
        try
        {
            group.Name = request.name;
            group.CostPractice = request.costPractice;
            
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return new OkObjectResult(null);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка обновлении записи" });
        }
    }

    public async Task<ObjectResult> AddStudent(Guid groupId, Guid studentId)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        
        try
        {
            var studentGroup = new StudentGroup(groupId, studentId);
            await db.StudentsGroups.AddAsync(studentGroup);
            
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return new OkObjectResult(null);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка. Группы или пользователя не существует" });
        }
    } 
    
    public async Task<ObjectResult> DeleteGroup(Guid groupId)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        
        try
        {
            var group = await db.Groups.FindAsync(groupId);
            db.Groups.Remove(group);
        
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
        
            return new OkObjectResult(null);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка. Группы или пользователя не существует" });
        }
    }
}