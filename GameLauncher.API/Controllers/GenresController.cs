using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _Service;
    public GenresController(IGenreService itemService)
    {
        _Service = itemService;
    }
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(_Service.GetAll());
    }
    [HttpGet("ByItem/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        return Ok(_Service.GetAllForItem(id));
    }
    [HttpPost("ChangeGenreForItem")]
    public async Task<ActionResult> UpdateGenreFotItem([FromBody] UpdateGenreMessage Message)
    {
        _Service.UpdateGenreInItem(Message.Item, Message.newGenres);
        return Ok();
    }
}
