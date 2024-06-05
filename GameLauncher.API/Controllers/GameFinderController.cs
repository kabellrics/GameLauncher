using GameLauncher.DAL;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GameFinderController : ControllerBase
{
    private readonly ISteamGameFinderService SteamGameFinderService;
    private readonly IEAOriginGameFinderService EAOriginGameFinderService;
    private readonly IEpicGameFinderService EpicGameFinderService;
    public GameFinderController(ISteamGameFinderService steamGameFinderService, IEAOriginGameFinderService eAOriginGameFinderService, IEpicGameFinderService epicGameFinderService)
    {
        this.SteamGameFinderService = steamGameFinderService;
        this.EAOriginGameFinderService = eAOriginGameFinderService;
        this.EpicGameFinderService = epicGameFinderService;

    }
    [HttpGet("/GetSteamGame")]
    public async Task<ActionResult> GetSteamGame()
    {
       await SteamGameFinderService.GetGameAsync();
        return Ok();
    }
    [HttpGet("/GetEAOriginGame")]
    public async Task<ActionResult> GetEAOriginGame()
    {
       await EAOriginGameFinderService.GetGameAsync();
        return Ok();
    }
    [HttpGet("/GetEpicGame")]
    public async Task<ActionResult> GetEpicGame()
    {
       await EpicGameFinderService.GetGameAsync();
        return Ok();
    }
}
