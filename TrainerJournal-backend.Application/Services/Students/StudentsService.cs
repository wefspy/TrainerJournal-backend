using idunno.Password;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerJournal_backend.Application.Services.Students.DTOs;
using TrainerJournal_backend.Domain.Constants;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.Application.Services.Students;

public class StudentsService(
    AppDbContext db,
    UserManager<UserIdentity> userManager)
{
    public async Task<ObjectResult> CreateStudent(CreateStudentRequest request)
    {
        var group = await db.Groups.FindAsync(request.GroupId);
        if (group == null)
        {
            return new BadRequestObjectResult(new { message = "Группа не найдена" });
        }

        var passwordGenerator = new PasswordGenerator();
        var password = passwordGenerator.Generate(10, 5, 5, false, true);

        var fullName =
            $"{request.StudentInfoItemDto.FirstName}{request.StudentInfoItemDto.LastName}{request.StudentInfoItemDto.MiddleName}";
        var userName = Transliteration.ConvertToTransliteration(fullName);

        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var newIdentity = new UserIdentity
            {
                UserName = userName,
                Email = request.StudentInfoItemDto.Email,
                PhoneNumber = request.StudentInfoItemDto.PhoneNumber,
            };
            var identityResult = await userManager.CreateAsync(newIdentity, password);
            if (!identityResult.Succeeded)
            {
                return new BadRequestObjectResult(new
                    { message = "Ошибка при создании пользователя", errors = identityResult.Errors });
            }

            await userManager.AddToRoleAsync(newIdentity, Roles.Student);

            var aikidoka = new Aikidoka(userName, request.StudentInfoItemDto.Kyu);
            var personName = new PersonName(
                request.StudentInfoItemDto.FirstName,
                request.StudentInfoItemDto.LastName,
                request.StudentInfoItemDto.MiddleName);

            var userInfo = new UserInfo(userName, personName.Id, request.StudentInfoItemDto.Gender);
            var studentInfo = new StudentInfo(request.StudentInfoItemDto.DateOfBirth,
                request.StudentInfoItemDto.Address,
                request.StudentInfoItemDto.Class, DateOnly.FromDateTime(DateTime.UtcNow));
            var wallet = new Wallet(0);

            var student = new Student(userName, wallet.Id, studentInfo.Id);

            await db.Aikidoki.AddAsync(aikidoka);
            await db.PeopleNames.AddAsync(personName);
            await db.UsersInfo.AddAsync(userInfo);
            await db.StudentsInfo.AddAsync(studentInfo);
            await db.Wallets.AddAsync(wallet);
            await db.Students.AddAsync(student);

            foreach (var cDto in request.Contacts)
            {
                var communication = new Communication(cDto.PhoneNumber, cDto.Email);
                var personNameContact = new PersonName(cDto.FirstName, cDto.LastName, cDto.MiddleName);

                await db.Communications.AddAsync(communication);
                await db.PeopleNames.AddAsync(personNameContact);

                var contact = new Contact(personNameContact.Id, communication.Id, cDto.Relation);
                await db.Contacts.AddAsync(contact);

                var studentContact = new StudentContact(student.Id, contact.Id);
                await db.StudentsContacts.AddAsync(studentContact);
            }

            var studentGroup = new StudentGroup(request.GroupId, student.Id);
            await db.StudentsGroups.AddAsync(studentGroup);

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult(new CreateStudentResponse(userName, password));
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при создании записей", error = ex });
        }
    }


    public async Task<ObjectResult> GetStudents(string userName)
    {
        var trainer = await db.Trainers
            .Include(t => t.Groups)
            .FirstOrDefaultAsync(t => t.UserName == userName);

        if (trainer == null)
        {
            return new NotFoundObjectResult("Тренер не найден");
        }

        var studentsGroups = await db.StudentsGroups
            .Where(sg => trainer.Groups.Select(g => g.Id).Contains(sg.GroupId))
            .Include(sg => sg.Student)
            .ThenInclude(s => s.UserIdentity)
            .ThenInclude(ui => ui.Aikidoka)
            .Include(sg => sg.Student.UserIdentity.UserInfo)
            .ThenInclude(ui => ui.PersonName)
            .Include(sg => sg.Student.Wallet)
            .Include(sg => sg.Group)
            .Include(sg => sg.Student.StudentInfo)
            .ToListAsync();

        var studentsGroupsDTO = studentsGroups.Select(sg =>
        {
            var student = sg.Student;
            var identity = student.UserIdentity;
            var aikidoka = identity.Aikidoka;
            var userInfo = identity.UserInfo;
            var personName = userInfo.PersonName;
            var studentInfo = student.StudentInfo;
            var wallet = student.Wallet;
            var group = sg.Group;

            var studentInfoDto = new StudentGroupDTO(
                identity.UserName,
                personName.FirstName,
                personName.LastName,
                personName.MiddleName,
                aikidoka.Kyu,
                studentInfo.DateOfBirth,
                studentInfo.Class,
                studentInfo.Address,
                identity.PhoneNumber,
                identity.Email,
                userInfo.Gender,
                wallet.Balance,
                group.Name
            );
            
            return studentInfoDto;
        }).ToList();

        return new OkObjectResult(studentsGroupsDTO);
    }

    public async Task<ObjectResult> GetUserInfo(string userName)
    {
        var Identity = await userManager.FindByNameAsync(userName);
        var aikidoka = await db.Aikidoki.FirstOrDefaultAsync(x => x.UserName == userName);
        var userInfo = await db.UsersInfo.FirstOrDefaultAsync(x => x.UserName == userName);
        var personName = await db.PeopleNames.FindAsync(userInfo.PersonNameId);
        var student = await db.Students.FirstOrDefaultAsync(x => x.UserName == userName);
        var studentInfo = await db.StudentsInfo.FindAsync(student.StudentInfoId);

        var studentInfoDto = new StudentInfoItemDTO(
            personName.FirstName,
            personName.LastName,
            personName.MiddleName,
            studentInfo.DateOfBirth,
            aikidoka.Kyu,
            studentInfo.Class,
            studentInfo.Address,
            Identity.PhoneNumber,
            Identity.Email,
            userInfo.Gender);

        return new OkObjectResult(studentInfoDto);
    }

    public async Task<ObjectResult> ChangeStudentInfo(string userName, StudentInfoItemDTO request)
    {
        var identity = await userManager.FindByNameAsync(userName);
        if (identity == null)
        {
            return new BadRequestObjectResult(new { message = "Пользователь не найден" });
        }

        var aikidoka = await db.Aikidoki.FirstOrDefaultAsync(x => x.UserName == userName);
        var userInfo = await db.UsersInfo.FirstOrDefaultAsync(x => x.UserName == userName);
        var personName = await db.PeopleNames.FindAsync(userInfo.PersonNameId);
        var student = await db.Students.FirstOrDefaultAsync(x => x.UserName == userName);
        var studentInfo = await db.StudentsInfo.FindAsync(student.StudentInfoId);

        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            personName.FirstName = request.FirstName;
            personName.LastName = request.LastName;
            personName.MiddleName = request.MiddleName;

            aikidoka.Kyu = request.Kyu;

            studentInfo.DateOfBirth = request.DateOfBirth;
            studentInfo.Class = request.Class;
            studentInfo.Address = request.Address;

            identity.Email = request.Email;
            identity.PhoneNumber = request.PhoneNumber;

            userInfo.Gender = request.Gender;

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult(request);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при обновлении записей" });
        }
    }

    public async Task<ObjectResult> GetContacts(string userName)
    {
        var student = await db.Students
            .Include(s => s.StudentContacts)
            .ThenInclude(sc => sc.Contact)
            .ThenInclude(c => c.PersonName)
            .Include(s => s.StudentContacts)
            .ThenInclude(sc => sc.Contact)
            .ThenInclude(c => c.Communication)
            .FirstOrDefaultAsync(x => x.UserName == userName);

        if (student == null)
        {
            return new NotFoundObjectResult("Student not found");
        }

        var contacts = student.StudentContacts.Select(sc =>
            {
                var contact = sc.Contact;
                var personName = contact.PersonName;
                var communication = contact.Communication;

                var contactItemDto = new ContactItemDTO(
                    personName.FirstName,
                    personName.LastName,
                    personName.MiddleName,
                    communication.PhoneNumber,
                    communication.Email,
                    contact.Relation);

                return new ContactDTO(contact.Id, contactItemDto);
            })
            .ToList();

        return new OkObjectResult(contacts);
    }

    public async Task<ObjectResult> ChangeContacts(List<ContactDTO> request)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        var contactIds = request.Select(x => x.ContactId).ToList();
        var contacts = await db.Contacts
            .Where(c => contactIds.Contains(c.Id))
            .Include(c => c.PersonName)
            .Include(c => c.Communication)
            .ToListAsync();

        try
        {
            foreach (var item in request)
            {
                var contact = contacts.FirstOrDefault(c => c.Id == item.ContactId);
                if (contact != null)
                {
                    var personName = contact.PersonName;
                    var communication = contact.Communication;

                    contact.Relation = item.ContactItem.Relation;
                    personName.FirstName = item.ContactItem.FirstName;
                    personName.LastName = item.ContactItem.LastName;
                    personName.MiddleName = item.ContactItem.MiddleName;
                    communication.PhoneNumber = item.ContactItem.PhoneNumber;
                    communication.Email = item.ContactItem.Email;
                }
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult(request);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при обновлении записей" });
        }
    }
}