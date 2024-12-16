using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class GroupsConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        // Связь 1:M с StudentsGroups
        builder.HasMany(g => g.StudentsGroup)
            .WithOne(sg => sg.Group)
            .HasPrincipalKey(g => g.Id)
            .HasForeignKey(sg => sg.GroupId)
            .IsRequired();

        // Связь 1:M c Practices
        builder.HasMany(g => g.Practices)
            .WithOne(p => p.Group)
            .HasPrincipalKey(g => g.Id)
            .HasForeignKey(sg => sg.GroupId)
            .IsRequired();
    }
}