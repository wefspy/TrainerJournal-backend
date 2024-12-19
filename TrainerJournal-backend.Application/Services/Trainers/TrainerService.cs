using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerJournal_backend.Application.Services.Trainers.DTOs;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.Application.Services.Trainers;

public class TrainerService(
    AppDbContext db,
    UserManager<UserIdentity> userManager)
{
    public async Task<ObjectResult> GetByStudentUserName(string userName)
    {
        var student = await db.Students
            .Include(s => s.StudentGroups)
            .ThenInclude(s => s.Group)
            .ThenInclude(g => g.Trainer)
            .ThenInclude(t => t.UserIdentity)
            .ThenInclude(i => i.UserInfo)
            .ThenInclude(i => i.PersonName)
            .FirstOrDefaultAsync(s => s.UserName == userName);
        
        var trainers = student.StudentGroups.Select(g => g.Group.Trainer).Distinct().ToList();
        var trainerDtos = trainers.Select(t =>
        {
            var trainerName = t.UserIdentity.UserInfo.PersonName;
            return new TrainerDTO(t.Id, trainerName.FirstName, trainerName.LastName, trainerName.MiddleName);
        }).ToList();
        
        return new OkObjectResult(trainerDtos);
    }
    
    public async Task<ObjectResult> GetUserInfo(string userName)
    {
        var identity = await userManager.FindByNameAsync(userName);
        var userInfo = await db.UsersInfo.FirstOrDefaultAsync(i => i.UserName == userName);
        var personName = await db.PeopleNames.FindAsync(userInfo.PersonNameId);
        var aikidoka = await db.Aikidoki.FirstOrDefaultAsync(a => a.UserName == userName);
        
        var trainerInfo = new TrainerInfo(personName.FirstName, personName.LastName, personName.MiddleName,
            aikidoka.Kyu, identity.PhoneNumber, identity.Email);
        
        return new OkObjectResult(trainerInfo);
    }
    
    public async Task<ObjectResult> ChangeUserInfo(string userName, TrainerInfo request)
    {
        var identity = await userManager.FindByNameAsync(userName);
        var userInfo = await db.UsersInfo.FirstOrDefaultAsync(i => i.UserName == userName);
        var personName = await db.PeopleNames.FindAsync(userInfo.PersonNameId);
        var aikidoka = await db.Aikidoki.FirstOrDefaultAsync(a => a.UserName == userName);
        
        await using var transaction = await db.Database.BeginTransactionAsync();
        
        try
        {
            identity.Email = request.Email;
            identity.PhoneNumber = request.PhoneNumber;
            
            personName.FirstName = request.FirstName;
            personName.LastName = request.LastName;
            personName.MiddleName = request.MiddleName;
            
            aikidoka.Kyu = request.Kyu;
            
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
        
        return new OkObjectResult(request);
    }
}