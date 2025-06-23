using Microsoft.AspNetCore.Mvc;
using Notes.WebApp.Models;

namespace Notes.WebApp.Controllers;

[ApiController]
[Route("api/notes")]
public class NotesController : ControllerBase
{
    private static readonly List<Note> Notes = new();

    [HttpPost]
    public IActionResult Create([FromBody] CreateNoteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Title is required");

        var note = new Note
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Content = dto.Content
        };

        Notes.Add(note);
        return Ok(note);
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(Notes);
}

