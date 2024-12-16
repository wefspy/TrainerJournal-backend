using System.ComponentModel.DataAnnotations;

namespace TrainerJournal_backend.Application.Services.DTOs.Request;

public record SigInRequest(
    string UserName,
    string Password)
{

}