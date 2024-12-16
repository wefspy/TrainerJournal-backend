using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class StudentsInfoConfiguration : IEntityTypeConfiguration<StudentInfo>
{
    public void Configure(EntityTypeBuilder<StudentInfo> builder)
    {
        // Связь 1:1 с Students
        builder.HasOne(i => i.Student)
            .WithOne(s => s.StudentInfo)
            .HasPrincipalKey<StudentInfo>(i => i.Id)
            .HasForeignKey<Student>(s => s.StudentInfoId)
            .IsRequired();
    }
}