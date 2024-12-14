using Microsoft.AspNetCore.Identity;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет пользователя системы идентификации с <see cref="Guid"/> в качестве уникального идентификатора.
/// </summary>
public class UsersIdentity : IdentityUser<Guid>
{
}