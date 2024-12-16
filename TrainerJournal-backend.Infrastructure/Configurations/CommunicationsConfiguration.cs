using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class CommunicationsConfiguration : IEntityTypeConfiguration<Communication>
{
    public void Configure(EntityTypeBuilder<Communication> builder)
    {
        // Связь 1:1 с Contacts
        builder.HasOne(c => c.Contact)
            .WithOne(c => c.Communication)
            .HasPrincipalKey<Communication>(c => c.Id)
            .HasForeignKey<Contact>(c => c.CommunicationId)
            .IsRequired();
    }
}