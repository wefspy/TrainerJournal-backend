using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class ReceiptsConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        // Связь 1:1 с Payments
        builder.HasOne(r => r.Payment)
            .WithOne(p => p.Receipt)
            .HasPrincipalKey<Receipt>(r => r.Id)
            .HasForeignKey<Payment>(p => p.ReceiptId)
            .IsRequired();
    }
}