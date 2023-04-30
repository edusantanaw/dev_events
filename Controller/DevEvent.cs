using Microsoft.AspNetCore.Mvc;
using Entity;
using Persistence;
using Microsoft.EntityFrameworkCore;


[Route("/test")]
[ApiController]
public class DevEventsController : ControllerBase
{
    private readonly DevEventDbContext _context;

    [HttpGet]
    public IActionResult GetAll()
    {
        var devEvents = _context.Events.Where(d => d.IsDeleted == false).ToList();
        return Ok(devEvents);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var devEvent = _context.Events
        .Include(de => de.Speakers)
        .SingleOrDefault(d => d.id == id);

        if (devEvent == null) return NotFound();
        return Ok(devEvent);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, DevEvent devEvent)
    {
        var eventExists = _context.Events.SingleOrDefault(d => d.id == id);
        if (eventExists == null) return NotFound();
        eventExists.Update(devEvent.Title, devEvent.Description, devEvent.StartDate, devEvent.EndDate);
        _context.Events.Update(eventExists);
        _context.SaveChanges();
        return Ok(devEvent);
    }

    [HttpPost]
    public IActionResult Post(DevEvent devEvent)
    {
        _context.Events.Add(devEvent);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { Id = devEvent.id }, devEvent);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var eventExists = _context.Events.SingleOrDefault(d => d.id == id);
        if (eventExists == null) return NotFound();
        eventExists.Delete();
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPost("{id}/speakers")]
    public IActionResult PostSpeaker(Guid id, DevEventSpeaker speaker)
    {
        var devEvent = _context.Events.Any(d => d.id == id);
        if (!devEvent)
        {
            return NotFound();
        }
        _context.Speakers.Add(speaker);
        _context.SaveChanges();
        return NoContent();
    }

    public DevEventsController(DevEventDbContext context)
    {
        _context = context;
    }
}