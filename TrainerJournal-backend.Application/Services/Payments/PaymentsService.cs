using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerJournal_backend.Application.Services.Payments.DTOs;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Domain.Enums;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.Application.Services.Payments;

public class PaymentsService(
    IWebHostEnvironment webHostEnvironment,
    AppDbContext db)
{
    public async Task<ObjectResult> Create(string studentUserName, CreatePaymentDTO request)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            if (request.File == null || request.File.Length == 0)
            {
                return new BadRequestObjectResult("File is required.");
            }

            var receipt = new Receipt("");
            
            var uploadsFolder = Path.Combine(webHostEnvironment.ContentRootPath, "uploads", receipt.Id.ToString());
            Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, request.File.FileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            receipt.Url = $"http://localhost:5001/uploads/{receipt.Id}/{request.File.FileName}";
            
            await db.AddAsync(receipt);
            await db.SaveChangesAsync();

            var student = await db.Students
                .Include(s => s.Wallet)
                .FirstOrDefaultAsync(s => s.UserName == studentUserName);

            if (student == null)
            {
                return new NotFoundObjectResult("Student not found.");
            }

            var trainer = await db.Trainers.FirstOrDefaultAsync(t => t.Id == request.TrainerId);
            if (trainer == null)
            {
                return new BadRequestObjectResult("Trainer not found with the specified TrainerId.");
            }

            var paymentInfo = new PaymentInfo(
                DateOnly.FromDateTime(DateTime.UtcNow),
                request.Amount,
                PaymentStatus.UnderConsideration);

            await db.PaymentsInfo.AddAsync(paymentInfo);
            await db.SaveChangesAsync();

            var wallet = student.Wallet;
            var payment = new Payment(
                trainer.Id,
                receipt.Id,
                wallet.Id,
                paymentInfo.Id);

            await db.Payments.AddAsync(payment);

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult("File uploaded successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при сохранении файла", error = ex.Message });
        }
    }


    public async Task<ObjectResult> GetByStudentUserName(string userName)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        var student = await db.Students
            .Include(s => s.Wallet)
            .ThenInclude(w => w.Payments)
            .ThenInclude(p => p.Receipt)
            .Include(s => s.Wallet)
            .ThenInclude(w => w.Payments)
            .ThenInclude(p => p.PaymentInfo)
            .Include(s => s.Wallet)
            .ThenInclude(w => w.Payments)
            .ThenInclude(p => p.Trainer)
            .ThenInclude(t => t.UserIdentity)
            .ThenInclude(i => i.UserInfo)
            .ThenInclude(i => i.PersonName)
            .FirstOrDefaultAsync(s => s.UserName == userName);

        if (student == null)
        {
            return new NotFoundObjectResult("User not found.");
        }

        var paymentItems = new List<PaymentStudentItemDTO>();
        var payments = student.Wallet.Payments;
        foreach (var payment in payments)
        {
            var info = payment.PaymentInfo;
            var receipt = payment.Receipt;
            var personName = payment.Trainer.UserIdentity.UserInfo.PersonName;
            var paymentItem = new PaymentStudentItemDTO(
                payment.Id,
                info.Date,
                personName.FirstName,
                personName.LastName,
                personName.MiddleName,
                info.Amount,
                info.Status,
                receipt.Url);
            paymentItems.Add(paymentItem);
        }

        return new OkObjectResult(paymentItems);
    }

    public async Task<ObjectResult> GetByTrainerUserName(string userName)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        var trainer = await db.Trainers
            .Include(t => t.Payments)
            .ThenInclude(p => p.Receipt)
            .Include(t => t.Payments)
            .ThenInclude(p => p.PaymentInfo)
            .Include(t => t.Payments)
            .ThenInclude(p => p.Wallet)
            .ThenInclude(w => w.Student)
            .ThenInclude(s => s.UserIdentity)
            .ThenInclude(i => i.UserInfo)
            .ThenInclude(i => i.PersonName)
            .Include(t => t.Payments)
            .ThenInclude(p => p.Wallet.Student.StudentGroups)
            .ThenInclude(s => s.Group)
            .FirstOrDefaultAsync(t => t.UserName == userName);

        if (trainer == null)
        {
            return new NotFoundObjectResult("User not found.");
        }

        var paymentItems = new List<PaymentTrainerItemDTO>();
        var payments = trainer.Payments;
        foreach (var payment in payments)
        {
            var info = payment.PaymentInfo;
            var receipt = payment.Receipt;
            var studentPersonName = payment.Wallet.Student.UserIdentity.UserInfo.PersonName;
            var groupsNames = payment.Wallet.Student.StudentGroups.Select(g => g.Group.Name).ToList();

            var paymentItem = new PaymentTrainerItemDTO(
                payment.Id,
                studentPersonName.FirstName,
                studentPersonName.LastName,
                studentPersonName.MiddleName,
                groupsNames,
                info.Amount,
                info.Status,
                receipt.Url);
            paymentItems.Add(paymentItem);
        }

        return new OkObjectResult(paymentItems);
    }

    public async Task<ObjectResult> Update(Guid paymentId, UpdatePaymentDTO updatePaymentDto)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var payment = await db.Payments
                .Include(p => p.PaymentInfo)
                .Include(p => p.Wallet)
                .FirstOrDefaultAsync(p => p.Id == paymentId);
            if (payment == null)
            {
                return new BadRequestObjectResult("Платеж не существует");
            }

            if (payment.PaymentInfo.Status == updatePaymentDto.Status)
            {
                return new BadRequestObjectResult("Указанный статус уже установлен");
            }
            
            if (updatePaymentDto.Status == PaymentStatus.Approved)
            {
                payment.Wallet.Balance += payment.PaymentInfo.Amount;
            }
            else if (payment.PaymentInfo.Status == PaymentStatus.Approved && updatePaymentDto.Status == PaymentStatus.Rejected)
            {
                payment.Wallet.Balance -= payment.PaymentInfo.Amount;
            }
                
            payment.PaymentInfo.Status = updatePaymentDto.Status;
            
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return new OkObjectResult(new { message = "Баланс обновлен" });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при обновлении записи" });
        }
    }

    public async Task<ObjectResult> Delete(Guid paymentId)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var payment = await db.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                return new BadRequestObjectResult("Платеж не существует");
            }

            var delEntity = db.Payments.Remove(payment).Entity;

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new OkObjectResult(delEntity.Id);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new BadRequestObjectResult(new { message = "Ошибка при удалении записи" });
        }
    }
}