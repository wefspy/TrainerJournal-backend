using System.ComponentModel.DataAnnotations;

namespace TrainerJournal_backend.Application.Services.DTOs.Response;

public record SignInResponse(
    string UserName,
    string Token,
    IList<string> Roles
    )
{

}