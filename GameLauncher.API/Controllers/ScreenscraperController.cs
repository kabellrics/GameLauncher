using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ScreenscraperController : ControllerBase
{
    private readonly IScreenscraperService _Service;
    public ScreenscraperController(IScreenscraperService itemService)
    {
        _Service = itemService;
    }
    [HttpGet("SearchByName/{name}")]
    public async Task<ActionResult> SearchGameByName(string name)
    {
        return Ok(_Service.SearchGameByName(name));
    }
    [HttpGet("SearchGameByFileName/{filename}")]
    public async Task<ActionResult> SearchGameByFileName(string filename)
    {
        return Ok(_Service.SearchGameByFileName(filename));
    }
}
