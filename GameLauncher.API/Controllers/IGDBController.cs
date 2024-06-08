using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IGDBController : ControllerBase
{
    private readonly IIGDBService _Service;
    public IGDBController(IIGDBService itemService)
    {
        _Service = itemService;
    }
    [HttpGet("GetGameByName/{name}")]
    public async Task<ActionResult> GetGameByName(string name)
    {
        return Ok(_Service.GetGameByName(name));
    }
    [HttpGet("GetDetailsGame/{id}")]
    public async Task<ActionResult> GetDetailsGame(int id)
    {
        return Ok(_Service.GetDetailsGame(id));
    }
    [HttpGet("GetGameVideo/{id}")]
    public async Task<ActionResult> GetGameVideo(int id)
    {
        return Ok(_Service.GetVideosByGameId(id));
    }
    [HttpGet("GetCompaniesName/{ids}")]
    public async Task<ActionResult> GetCompaniesName(string ids)
    {
        return Ok(_Service.GetCompaniesDetail(ids.Split(',').ToList()));
    }
}
