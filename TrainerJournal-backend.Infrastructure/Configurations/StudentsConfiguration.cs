using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Infrastructure.Configurations;

public class StudentsConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        // Связь 1:M с StudentsGroups
        builder.HasMany(s => s.StudentGroups)
            .WithOne(sg => sg.Student)
            .HasPrincipalKey(s => s.Id)
            .HasForeignKey(sg => sg.StudentId)
            .IsRequired();
        
        // Связь 1:M с AttendancePractices
        builder.HasMany(s => s.AttendancePractices)
            .WithOne(atp => atp.Student)
            .HasPrincipalKey(s => s.Id)
            .HasForeignKey(sg => sg.StudentId)
            .IsRequired();
        
        // Связь 1:M с StudentsContacts
        builder.HasMany(s => s.StudentContacts)
            .WithOne(sc => sc.Student)
            .HasPrincipalKey(s => s.Id)
            .HasForeignKey(sg => sg.StudentId)
            .IsRequired();
    }
}