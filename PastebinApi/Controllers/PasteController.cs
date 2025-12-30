using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastebinApi.Data;
using PastebinApi.Models;
using PastebinApi.Dtos;

namespace PastebinApi.Controllers;

[ApiController]
[Route("api/paste")]
public class PasteController : ControllerBase
{
    private readonly AppDbContext _db;
    public PasteController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Create(CreatePasteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Content))
            return BadRequest("Content required");

        var paste = new Paste
        {
            Content = dto.Content,
            ExpiresAt = dto.ExpiresInMinutes != null
                ? DateTime.UtcNow.AddMinutes(dto.ExpiresInMinutes.Value)
                : null,
            MaxViews = dto.MaxViews
        };

        _db.Pastes.Add(paste);
        await _db.SaveChangesAsync();

        return Ok(new { id = paste.Id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var paste = await _db.Pastes.FindAsync(id);
        if (paste == null) return NotFound();

        if (paste.ExpiresAt != null && paste.ExpiresAt < DateTime.UtcNow)
            return StatusCode(410, "Expired");

        if (paste.MaxViews != null && paste.CurrentViews >= paste.MaxViews)
            return StatusCode(410, "View limit reached");

        paste.CurrentViews++;
        await _db.SaveChangesAsync();

        return Ok(new { content = paste.Content });
    }
}
