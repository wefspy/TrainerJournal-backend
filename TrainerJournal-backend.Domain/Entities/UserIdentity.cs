using Microsoft.AspNetCore.Identity;

namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Представляет пользователя системы идентификации с <see cref="Guid"/> в качестве уникального идентификатора.
/// </summary>
public class UserIdentity : IdentityUser<Guid>
{
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Microsoft.AspNetCore.Identity.IdentityUser.UserName"/>
    /// </summary>
    public Aikidoka Aikidoka { get; private set; }
    
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Microsoft.AspNetCore.Identity.IdentityUser.UserName"/>
    /// </summary>
    public UserInfo UserInfo { get; private set; }
    
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Microsoft.AspNetCore.Identity.IdentityUser.UserName"/>
    /// </summary>
    public Student Student { get; private set; }
    
    /// <summary>
    /// Ссылка на объект внешнего ключа <see cref="Microsoft.AspNetCore.Identity.IdentityUser.UserName"/>
    /// </summary>
    public Trainer Trainer { get; private set; }
}