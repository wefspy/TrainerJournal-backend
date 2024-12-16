using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services;
using TrainerJournal_backend.Application.Services.DTOs.Request;
using TrainerJournal_backend.Application.Services.DTOs.Response;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    AuthService authService) : ControllerBase
{
    [HttpPost("signin")]
    public async Task<ActionResult<SignInResponse>> SignIn(SigInRequest request)
    {
        return await authService.SignInAsync(request);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<SignInResponse>> Register(RegisterTrainerRequest request)
    {
        return await authService.RegisterTrainer(request);
    }
}