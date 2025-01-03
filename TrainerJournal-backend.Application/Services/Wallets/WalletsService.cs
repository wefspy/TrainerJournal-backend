using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerJournal_backend.Application.Services.Wallets.DTOs;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.Application.Services.Wallets;

public class WalletsService(
    AppDbContext db)
{
    public async Task<ObjectResult> GetByUserName(string userName)
    {
        var student = await db.Students
            .Include(s => s.Wallet)
            .FirstOrDefaultAsync(s => s.UserName == userName);

        if (student == null)
        {
            return new NotFoundObjectResult(new { message = "Студент не найдена" });
        }

        var wallet = student.Wallet;
        var walletDto = new WalletDTO(wallet.Balance);
        
        return new ObjectResult(walletDto);
    }
}