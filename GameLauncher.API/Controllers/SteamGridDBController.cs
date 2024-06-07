using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SteamGridDBController : ControllerBase
{
    private readonly ISteamGridDbService _Service;
    public SteamGridDBController(ISteamGridDbService itemService)
    {
        _Service = itemService;
    }
    [HttpGet("SearchByName/{name}")]
    public async Task<ActionResult> SearchByName(string name)
    {
        return Ok(_Service.SearchByName(name));
    }
    [HttpGet("GetGameSteamId/{steamid}")]
    public async Task<ActionResult> GetGameSteamId(string steamid)
    {
        return Ok(_Service.GetGameSteamId(steamid));
    }
    [HttpGet("GetHeroesForId/{gameid}")]
    public async Task<ActionResult> GetHeroesForId(int gameid)
    {
        return Ok(_Service.GetHeroesForId(gameid));
    }
    [HttpGet("GetLogoForId/{gameid}")]
    public async Task<ActionResult> GetLogoForId(int gameid)
    {
        return Ok(_Service.GetLogoForId(gameid));
    }
    [HttpGet("GetGridBoxartForId/{gameid}")]
    public async Task<ActionResult> GetGridBoxartForId(int gameid)
    {
        return Ok(_Service.GetGridBoxartForId(gameid));
    }
}
