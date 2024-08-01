using System.Text.Json;
using System.Text;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Implementation;
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
    [HttpGet("GetDefaultCollectionStatus")]
    public async Task<ActionResult> GetDefaultCollectionStatus()
    {
        return Ok(_Service.GetDefaultCollectionStatus());
    }
    [HttpGet("GetPredefineCollection")]
    public async Task<ActionResult> GetPredefineCollection()
    {
        return Ok(_Service.GetPredefineCollection());
    }
    [HttpPost("CreateDefaultCollection")]
    public async Task<ActionResult> CreateDefaultCollection([FromBody] DefaultCollectionMessage collectionMessage)
    {
        return Ok(_Service.CreateDefaultColection(collectionMessage));
    }
    [HttpGet("GetAllItemInsideStream/{id}")]
    public async Task StreamJSON(Guid id)
    {
        Response.ContentType = "application/x-ndjson"; // Set the content type to NDJSON

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false // Ensure single-line JSON
        };

        await foreach (var item in _Service.GetAllItemInside(id))
        {
            var json = System.Text.Json.JsonSerializer.Serialize(item, options);
            await Response.WriteAsync(json + "\n", Encoding.UTF8);
            await Response.Body.FlushAsync(); // Ensure the response is sent immediately
        }
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
        _Service.UpsertCollectionItem(id, gameid, newOrder);
        return Ok();
    }
    [HttpPost]
    public ActionResult Post([FromBody] Collection value)
    {
        if (ModelState.IsValid)
        {
            _Service.CreateCollection(value);
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
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
    [HttpDelete("CollectionItem/{id}")]
    public ActionResult DeleteCollecItem(Guid id)
    {
        try
        {
            return Ok(
            _Service.DelteCollectionItem(id));
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteCollec(Guid id)
    {
        try
        {
            return Ok(
            _Service.DelteCollection(id));
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
}
