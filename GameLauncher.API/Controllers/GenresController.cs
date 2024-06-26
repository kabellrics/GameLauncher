﻿using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [HttpGet("Fusion/{idToDelete}/{idToKeep}")]
    public async Task<ActionResult> Fusion(Guid idToDelete, Guid idToKeep)
    {
        _Service.Fusionnage(idToDelete, idToKeep);
        return Ok();
    }
    [HttpPost("ChangeGenreForItem")]
    public async Task<ActionResult> UpdateGenreFotItem([FromBody] UpdateGenreMessage Message)
    {
        _Service.UpdateGenreInItem(Message.Item, Message.newGenres);
        return Ok();
    }
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] Genre todoItem)
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
