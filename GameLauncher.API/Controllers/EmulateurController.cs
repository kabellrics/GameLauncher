using System.Text.Json;
using System.Text;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLauncher.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmulateurController : ControllerBase
{
    private readonly IEmulateurService _Service;
    public EmulateurController(IEmulateurService itemService)
    {
        _Service = itemService;
    }
    [HttpGet("GetLocalEmu")]
    public IEnumerable<LUEmulateur> GetLocalEmu()
    {
        return _Service.GetLocalEmulator();
    }
    [HttpGet("GetLocalProfile")]
    public IEnumerable<LUProfile> GetLocalProfile()
    {
        return _Service.GetLocalEmulatorProfile();
    }
    [HttpGet("ScanFolder/{folder}")]
    public IEnumerable<LUEmulateur> ScanFolder(string folder)
    {
        return _Service.ScanFolderForEmulator(folder);
    }
    [HttpGet("StreamFolder/{folder}")]
    public async Task StreamFolder(string folder)
    {
        Response.ContentType = "application/x-ndjson"; // Set the content type to NDJSON

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false // Ensure single-line JSON
        };

        await foreach (var item in _Service.RecursiveScanAsync(folder))
        {
            var json = System.Text.Json.JsonSerializer.Serialize(item, options);
            await Response.WriteAsync(json + "\n", Encoding.UTF8);
            await Response.Body.FlushAsync(); // Ensure the response is sent immediately
        }
    }
}
