using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class RolesIdentityConfiguration : IEntityTypeConfiguration<RoleIdentity>
{
    public void Configure(EntityTypeBuilder<RoleIdentity> builder)
    {
        var roles = new[]
        {
            Domain.Constants.Roles.Student, 
            Domain.Constants.Roles.Trainer
        };
        
        var identityRoles = roles.Select(role => 
            new RoleIdentity
            {
                Id = Guid.NewGuid(), 
                Name = role, 
                NormalizedName = role.ToUpper(), 
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

        builder.HasData(identityRoles);
    }
}