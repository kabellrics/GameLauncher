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
public class IntroVideoController : ControllerBase
{
    private readonly IVideoIntroService _Service;
    public IntroVideoController(IVideoIntroService Service)
    {
        _Service = Service;
    }
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(_Service.GetIntroVideos());
    }
    [HttpGet("RandomIntro")]
    public async Task<ActionResult> GetRandomIntro()
    {
        return Ok(_Service.GetRandomVideoIntro());
    }
    [HttpPost]
    public ActionResult Post([FromBody] FileRequest value)
    {
        if (ModelState.IsValid)
        {
            _Service.InsertIntroVideo(value.SourceFile,value.NameFile);
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
        try
        {
            _Service.DeleteIntroVideo(id);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] IntroVideo todoItem)
    {
        if (id != todoItem.ID)
        {
            return BadRequest();
        }
        try
        {
            _Service.UpdateItem(todoItem);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
}
