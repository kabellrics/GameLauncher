using System.Text.Json;
using System.Text;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Implementation;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NexusMods.Paths;

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
    public async IAsyncEnumerable<Item> Stream()
    {        
        await foreach(var item in _itemService.GetAllAsync()){
            yield return item;
        }
    }
    [HttpGet("StreamJSON")]
    public async Task StreamJSON()
    {
        Response.ContentType = "application/x-ndjson"; // Set the content type to NDJSON

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false // Ensure single-line JSON
        };

        await foreach (var item in _itemService.GetAllAsync())
        {
            var json = System.Text.Json.JsonSerializer.Serialize(item, options);
            await Response.WriteAsync(json + "\n", Encoding.UTF8);
            await Response.Body.FlushAsync(); // Ensure the response is sent immediately
        }
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
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
        try
        {
            _itemService.DeleteItem(id);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
}
