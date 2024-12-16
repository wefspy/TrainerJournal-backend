using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class PaymentsInfoConfiguration : IEntityTypeConfiguration<PaymentInfo>
{
    public void Configure(EntityTypeBuilder<PaymentInfo> builder)
    {
        // Связь 1:1 с Payments
        builder.HasOne(i => i.Payment)
            .WithOne(p => p.PaymentInfo)
            .HasPrincipalKey<PaymentInfo>(i => i.Id)
            .HasForeignKey<Payment>(p => p.PaymentInfoId)
            .IsRequired();
    }
}