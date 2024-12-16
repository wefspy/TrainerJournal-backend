using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class ContactsConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        // Связь 1:M с StudentsContacts
        builder.HasMany(c => c.StudentContacts)
            .WithOne(sc => sc.Contact)
            .HasPrincipalKey(c => c.Id)
            .HasForeignKey(sc => sc.ContactId)
            .IsRequired();
    }
}