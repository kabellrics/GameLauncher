using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DevController : ControllerBase
{
    private readonly IDevService _Service;
    public DevController(IDevService itemService)
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
