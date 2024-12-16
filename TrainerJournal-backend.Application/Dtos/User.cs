using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Application.Dtos;

public class User
{
    public Guid Id { get; init; }
    
    public string UserName { get; init; }

    public List<string> Roles { get; init; } = null!;
    
    public string FirstName { get; init; } = null!;
    
    public string LastName { get; init; } = null!;
    
    public string MiddleName { get; init; } 
    
    public Gender Gender { get; init; }
    
    public string Email { get; init; }
    
    public string PhoneNumber { get; init; }
}