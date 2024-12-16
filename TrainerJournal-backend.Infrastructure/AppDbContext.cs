using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Infrastructure.Configurations;

namespace TrainerJournal_backend.Infrastructure;

public class AppDbContext : IdentityDbContext<
    UserIdentity, 
    RoleIdentity, 
    Guid>
{
    public DbSet<Aikidoka> Aikidoki { get; set; }
    public DbSet<AttendancePractice> AttendancePractices { get; set; }
    public DbSet<Communication> Communications { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentInfo> PaymentsInfo { get; set; }
    public DbSet<PersonName> PeopleNames { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentContact> StudentsContacts { get; set; }
    public DbSet<StudentGroup> StudentsGroups { get; set; }
    public DbSet<StudentInfo> StudentsInfo { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Practice> Practices { get; set; }
    public DbSet<UserInfo> UsersInfo { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CommunicationsConfiguration());
        builder.ApplyConfiguration(new ContactsConfiguration());
        builder.ApplyConfiguration(new GroupsConfiguration());
        builder.ApplyConfiguration(new PaymentsInfoConfiguration());
        builder.ApplyConfiguration(new PeopleNamesConfiguration());
        builder.ApplyConfiguration(new PracticesConfiguration());
        builder.ApplyConfiguration(new ReceiptsConfiguration());
        builder.ApplyConfiguration(new StudentsConfiguration());
        builder.ApplyConfiguration(new TrainersConfiguration());
        builder.ApplyConfiguration(new UsersIdentityConfiguration());
        builder.ApplyConfiguration(new WalletsConfiguration());
        builder.ApplyConfiguration(new RolesIdentityConfiguration());
        
        base.OnModelCreating(builder);
    }
}