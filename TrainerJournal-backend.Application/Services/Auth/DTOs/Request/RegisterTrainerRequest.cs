using TrainerJournal_backend.Application.Services.Trainers.DTOs;

namespace TrainerJournal_backend.Application.Services.DTOs.Request;

public record RegisterTrainerRequest(
    string UserName,
    string Password,
    TrainerInfo TrainerInfo
    )
{
    
}