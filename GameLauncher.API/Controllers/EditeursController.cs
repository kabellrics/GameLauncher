using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EditeursController : ControllerBase
{
    private readonly IEditeurService _Service;
    public EditeursController(IEditeurService itemService)
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
    [HttpPost("ChangeEditeurForItem")]
    public async Task<ActionResult> UpdateEditeurFotItem([FromBody] UpdateEditeurMessage Message)
    {
        _Service.UpdateEditeurInItem(Message.Item, Message.newEditeurs);
        return Ok();
    }
}
