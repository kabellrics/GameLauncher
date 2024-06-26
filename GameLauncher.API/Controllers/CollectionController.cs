using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CollectionController : ControllerBase
{
    private readonly ICollectionService _Service;
    public CollectionController(ICollectionService itemService)
    {
        _Service = itemService;
    }
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(_Service.GetAll());
    }
    [HttpGet("GetAllItemInside/{id}")]
    public IAsyncEnumerable<ItemInCollection> GetAllItemInside(Guid id)
    {
        return _Service.GetAllItemInside(id);
    }
    [HttpGet("CreateCollectionFromPlateforme")]
    public async Task<ActionResult> CreateCollectionFromPlateforme()
    {
        _Service.CreateCollectionFromPlateforme();
        return Ok();
    }
    [HttpGet("AddToCollectionEnd/{id}/{gameid}")]
    public async Task<ActionResult> AddToCollectionEnd(Guid id, Guid gameid)
    {
        _Service.AddToCollectionEnd(id, gameid);
        return Ok();
    }
    [HttpGet("UpdateCollectionItemOrder/{id}/{gameid}/{newOrder}")]
    public async Task<ActionResult> UpdateCollectionItemOrder(Guid id, Guid gameid,int newOrder)
    {
        _Service.UpdateCollectionItemOrder(id, gameid, newOrder);
        return Ok();
    }
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] Collection todoItem)
    {
        if (id != todoItem.ID)
        {
            return BadRequest();
        }
        try
        {
            _Service.Update(todoItem);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
}
