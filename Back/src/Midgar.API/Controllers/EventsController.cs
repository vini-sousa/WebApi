using Microsoft.AspNetCore.Mvc;
using Midgar.Domain;
using Midgar.Application.Interface;

namespace Midgar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var events = await _eventService.GetAllEventsAsync(true);

            if (events == null)
                return NotFound("No events found.");
            
            return Ok(events);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve events. Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var eventById = await _eventService.GetEventByIdAsync(id, true);

            if (eventById == null)
                return NotFound("Event by Id not found.");
            
            return Ok(eventById);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve events. Error: {ex.Message}");
        }
    }

    [HttpGet("{theme}/theme")]
    public async Task<IActionResult> GetByTheme(string theme)
    {
        try
        {
            var eventById = await _eventService.GetAllEventsByThemeAsync(theme, true);

            if (eventById == null)
                return NotFound("Events by theme not found.");
            
            return Ok(eventById);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve events. Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Event model)
    {
        try
        {
            var eventPost = await _eventService.AddEvents(model);

            if (eventPost == null)
                return BadRequest("Error when trying to add event.");
            
            return Ok(eventPost);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to add events. Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Event model)
    {
        try
        {
            var eventPut = await _eventService.UpdateEvents(id, model);

            if (eventPut == null)
                return BadRequest("Error when trying to update event.");
            
            return Ok(eventPut);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to update events. Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _eventService.DeleteEvents(id) ? Ok("Deleted") : BadRequest("Event not deleted");
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to delete events. Error: {ex.Message}");
        }
    }
}