using GameLauncher.DAL;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GameFinderController : ControllerBase
{
    private readonly ISteamGameFinderService steamGameFinderService;
    public GameFinderController(ISteamGameFinderService steamGameFinderService)
    {
        this.steamGameFinderService = steamGameFinderService;
    }
    [HttpGet("/GetSteamGame")]
    public async Task<ActionResult> GetSteamGame()
    {
       await steamGameFinderService.GetGameAsync();
        return Ok();
    }
}
