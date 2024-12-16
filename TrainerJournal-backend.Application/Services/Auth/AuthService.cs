using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.DTOs.Request;
using TrainerJournal_backend.Application.Services.DTOs.Response;
using TrainerJournal_backend.Application.Services.Jwt;
using TrainerJournal_backend.Domain.Constants;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Domain.Enums;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.Application.Services;

public class AuthService(
    AppDbContext db,
    UserManager<UserIdentity> userManager, 
    SignInManager<UserIdentity> signInManager, 
    JwtGenerator jwtGenerator)
{
    public async Task<ObjectResult> SignInAsync(SigInRequest request)
    {
        var identity = await userManager.FindByNameAsync(request.UserName);
        if (identity == null)
        {
            return new NotFoundObjectResult(new { Message = "User not found" });
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(identity, request.Password);
        if (!isPasswordValid)
        {
            return new BadRequestObjectResult(new { Message = "Bad credentials" });
        }

        var roles = await userManager.GetRolesAsync(identity);
        var token = jwtGenerator.GenerateToken(identity, roles);
        await signInManager.SignInAsync(identity, false);

        return new OkObjectResult(new SignInResponse(identity.UserName, token, roles));
    }

    public async Task<ObjectResult> RegisterTrainer(RegisterTrainerRequest request)
    {
        var identity = await userManager.FindByNameAsync(request.UserName);
        if (identity != null)
        {
            return new BadRequestObjectResult(new { Message = "User already exists" });
        }
        
        await using var transaction = await db.Database.BeginTransactionAsync();
        
        try
        {
            var newIdentity = new UserIdentity()
            {
                UserName = request.UserName,
            };
            await userManager.CreateAsync(newIdentity, request.Password);
            await userManager.AddToRoleAsync(newIdentity, Roles.Trainer);
            
            var aikidoka = new Aikidoka(request.UserName, request.TrainerInfo.Kyu);
            await db.Aikidoki.AddRangeAsync(aikidoka);

            var personName = new PersonName(
                request.TrainerInfo.FirstName, request.TrainerInfo.LastName, request.TrainerInfo.MiddleName);
            db.PeopleNames.Add(personName);

            await db.SaveChangesAsync();

            var userInfo = new UserInfo(request.UserName, personName.Id, Gender.Male);
            db.UsersInfo.Add(userInfo);

            var trainer = new Trainer(request.UserName);
            db.Trainers.Add(trainer);
            
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }

        return new OkObjectResult(null);
    }
}