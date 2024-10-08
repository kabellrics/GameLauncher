﻿using GameLauncher.Services.Implementation;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StarterController : ControllerBase
{
    private readonly IStartingService _Service;
    public StarterController(IStartingService Service)
    {
        _Service = Service;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> Start(Guid id)
    {
        await _Service.StartITem(id);
        return Ok();
    }
}
