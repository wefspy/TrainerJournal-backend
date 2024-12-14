using Microsoft.AspNetCore.Identity;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет роль системы идентификации с <see cref="Guid"/> в качестве уникального идентификатора.
/// </summary>
public class RolesIdentity : IdentityRole<Guid>
{
}