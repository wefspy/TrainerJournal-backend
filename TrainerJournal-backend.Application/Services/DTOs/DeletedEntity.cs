using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Application.Services.DTOs;

public record DeletedEntity(
    Guid id)
{
    public DeletedEntity(Entity entity) : this(entity.Id)
    {
        
    }
}