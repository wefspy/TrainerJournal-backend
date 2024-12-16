using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class PeopleNamesConfiguration : IEntityTypeConfiguration<PersonName>
{
    public void Configure(EntityTypeBuilder<PersonName> builder)
    {
        // Связь 1:1 UserInfo
        builder.HasOne(p => p.UserInfo)
            .WithOne(u => u.PersonName)
            .HasPrincipalKey<PersonName>(p => p.Id)
            .HasForeignKey<UserInfo>(u => u.PersonNameId)
            .IsRequired();

        // Связь 1:1 Contacts
        builder.HasOne(p => p.Contact)
            .WithOne(c => c.PersonName)
            .HasPrincipalKey<PersonName>(p => p.Id)
            .HasForeignKey<Contact>(c => c.PersonNameId)
            .IsRequired();
    }
}