using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MetadataGenreController : ControllerBase
{
    private readonly IMetadataService _Service;
    public MetadataGenreController(IMetadataService itemService)
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
}
