using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class UsersIdentityConfiguration : IEntityTypeConfiguration<UserIdentity>
{
    public void Configure(EntityTypeBuilder<UserIdentity> builder)
    {
        // Связь 1:1 c Aikidoki
        builder.HasOne(i => i.Aikidoka)
            .WithOne(a => a.UserIdentity)
            .HasPrincipalKey<UserIdentity>(i => i.UserName)
            .HasForeignKey<Aikidoka>(a => a.UserName)
            .IsRequired();
        
        // Связь 1:1 с UsersInfo
        builder.HasOne(i => i.UserInfo)
            .WithOne(a => a.UserIdentity)
            .HasPrincipalKey<UserIdentity>(i => i.UserName)
            .HasForeignKey<UserInfo>(a => a.UserName)
            .IsRequired();
        
        // Связь 1:1 с Trainers
        builder.HasOne(i => i.Trainer)
            .WithOne(t => t.UserIdentity)
            .HasPrincipalKey<UserIdentity>(i => i.UserName)
            .HasForeignKey<Trainer>(a => a.UserName)
            .IsRequired(false);
        
        // Связь 1:1 с Students
        builder.HasOne(i => i.Student)
            .WithOne(t => t.UserIdentity)
            .HasPrincipalKey<UserIdentity>(i => i.UserName)
            .HasForeignKey<Student>(a => a.UserName)
            .IsRequired(false);
    }
}