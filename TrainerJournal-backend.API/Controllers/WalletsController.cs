using Microsoft.AspNetCore.Mvc;
using TrainerJournal_backend.Application.Services.Wallets;
using TrainerJournal_backend.Application.Services.Wallets.DTOs;

namespace TrainerJournal_backend.API.Controllers;

[ApiController]
[Route("api/wallets")]
public class WalletsController(
    WalletsService walletsService)
{
    [HttpGet]
    public async Task<ActionResult<WalletDTO>> GetByUserName(string userName)
    {
        return await walletsService.GetByUserName(userName);
    }
}