using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Midgar.API.Data;
using Midgar.API.Models;

namespace Midgar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly DataContext _context;

    public EventController(DataContext context)
    {
            this._context = context;
    }

    [HttpGet]
    public IEnumerable<Event> Get()
    {
        return _context.Events;
    }

    [HttpGet("{id}")]
    public Event GetById(int id)
    {
        return _context.Events.FirstOrDefault(evento => evento. EventId == id);
    }

    [HttpPost]
    public string Post()
    {
        return "Exemplo de Post";
    }

    [HttpPut("{id}")]
    public string Put(int id)
    {
        return $"Exemplo de Put com id = {id}";
    }

    [HttpDelete("{id}")]
    public string Delete(int id)
    {
        return $"Exemplo de Delete com id = {id}";
    }
}
