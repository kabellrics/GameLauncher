using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Implementation;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [HttpGet("Stream")]
    public IAsyncEnumerable<Item> Stream()
    {        
        return _itemService.GetAllAsync();
    }
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] Item todoItem)
    {
        if (id != todoItem.ID)
        {
            return BadRequest();
        }
        try
        {
            _itemService.UpdateItem(todoItem);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
}
