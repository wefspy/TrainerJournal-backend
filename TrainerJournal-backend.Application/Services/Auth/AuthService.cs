using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.DTOs.Request;
using TrainerJournal_backend.Application.Services.DTOs.Response;
using TrainerJournal_backend.Application.Services.Jwt;
using TrainerJournal_backend.Domain.Constants;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Application.Services;

public class AuthService(
    UserManager<UserIdentity> userManager, 
    SignInManager<UserIdentity> signInManager, 
    JwtGenerator jwtGenerator)
{
    public async Task<ObjectResult> SignInAsync(SigInRequest request)
    {
        var identity = await userManager.FindByNameAsync(request.UserName);
        if (identity == null)
        {
            return new ObjectResult(new { Message = "User not found" })
            {
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(identity, request.Password);
        if (!isPasswordValid)
        {
            return new ObjectResult(new { Message = "Bad credentials" })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        var roles = await userManager.GetRolesAsync(identity);
        var token = jwtGenerator.GenerateToken(identity, roles);
        await signInManager.SignInAsync(identity, false);
        
        return new ObjectResult(new SignInResponse(identity.UserName, token, roles))
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<ObjectResult> RegisterTrainer(RegisterTrainerRequest request)
    {
        var identity = await userManager.FindByNameAsync(request.UserName);
        if (identity != null)
        {
            return new ObjectResult(new { Message = "User already exists" })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        var newIdentity = new UserIdentity()
        {
            UserName = request.UserName,
        };
            
        await userManager.CreateAsync(newIdentity, request.Password);
        await userManager.AddToRoleAsync(newIdentity, Roles.Trainer);

        return new ObjectResult(null)
        {
            StatusCode = StatusCodes.Status200OK
        };
    }
}