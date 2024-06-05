using GameLauncher.DAL;
using GameLauncher.Services.Implementation;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IItemsService _itemService;
    public ItemsController(IItemsService itemService)
    {
        _itemService = itemService;
    }
    [HttpGet]
    public async Task<ActionResult> Get()
    {        
        return Ok(_itemService.GetAll());
    }
}
