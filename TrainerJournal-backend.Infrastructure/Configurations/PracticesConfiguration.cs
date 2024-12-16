using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class PracticesConfiguration : IEntityTypeConfiguration<Practice>
{
    public void Configure(EntityTypeBuilder<Practice> builder)
    {
        // Связь 1:M с AttendancePractices
        builder.HasMany(p => p.AttendancePractices)
            .WithOne(a => a.Practice)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(a => a.PracticeId)
            .IsRequired();
    }
}