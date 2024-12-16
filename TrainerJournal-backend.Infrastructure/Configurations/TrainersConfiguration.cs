using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class TrainersConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        // Связь 1:M с Groups
        builder.HasMany(t => t.Groups)
            .WithOne(g => g.Trainer)
            .HasPrincipalKey(t => t.Id)
            .HasForeignKey(g => g.TrainerId)
            .IsRequired();
        
        // Связь 1:M с Payments
        builder.HasMany(t => t.Payments)
            .WithOne(p => p.Trainer)
            .HasPrincipalKey(t => t.Id)
            .HasForeignKey(t => t.TrainerId)
            .IsRequired();
    }
}