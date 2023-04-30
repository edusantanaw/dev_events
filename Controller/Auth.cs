using Microsoft.AspNetCore.Mvc;
using Entity;
using Persistence;


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
        var devEvent = _context.Events.SingleOrDefault(d => d.id == id);
        if (devEvent == null) return NotFound();
        return Ok(devEvent);
    }

    [HttpGet("{id}")]
    public IActionResult Update(Guid id, DevEvent devEvent)
    {
        var eventExists = _context.Events.SingleOrDefault(d => d.id == id);
        if (eventExists == null) return NotFound();
        _context.Events.Where(d => d.id == id).First().Update(devEvent.Title, devEvent.Description, devEvent.StartDate, devEvent.EndDate);
        return Ok(devEvent);
    }

    [HttpPost]
    public IActionResult Post(DevEvent devEvent)
    {
        _context.Events.Add(devEvent);
        return CreatedAtAction(nameof(GetById), new { Id = devEvent.id }, devEvent);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var eventExists = _context.Events.SingleOrDefault(d => d.id == id);
        if (eventExists == null) return NotFound();
        _context.Events.Where(d => d.id == id).First().Delete();
        return NoContent();
    }
    public DevEventsController(DevEventDbContext context)
    {
        _context = context;
    }
}