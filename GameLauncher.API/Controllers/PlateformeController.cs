using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlateformeController : ControllerBase
{
    private readonly IPlateformeService _Service;
    public PlateformeController(IPlateformeService itemService)
    {
        _Service = itemService;
    }
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(_Service.GetAll());
    }
}
