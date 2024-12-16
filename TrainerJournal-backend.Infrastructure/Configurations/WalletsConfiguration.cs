using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class WalletsConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        // Связь 1:1 с Students
        builder.HasOne(w => w.Student)
            .WithOne(s => s.Wallet)
            .HasPrincipalKey<Wallet>(w => w.Id)
            .HasForeignKey<Student>(s => s.WalletId)
            .IsRequired();
        
        // Связь 1:M с Payments
        builder.HasMany(w => w.Payments)
            .WithOne(p => p.Wallet)
            .HasPrincipalKey(w => w.Id)
            .HasForeignKey(p => p.WalletId)
            .IsRequired();
    }
}