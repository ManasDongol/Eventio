using Eventing.ApiService.Controllers.Attendees.dto;
using Eventing.ApiService.Data.Entities;
using Eventing.ApiService.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventing.ApiService.Controllers.Attendees;

[ApiController]
public sealed class AttendeeController: ApiBaseController
{
    private readonly EventingDbContext _context;
    
    public AttendeeController(EventingDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [EndpointName("Add Attendee")]
    [EndpointDescription("Adding new attendees to event")]
    [EndpointSummary("Adding new attendees to event")]
    public async Task<IActionResult> AddAttendee(createAttendeeDto dto)
    {
        
        //first check if user and event exists before creating a new attenddee
        var userid = await _context.Users.AnyAsync(u => u.Id == dto.UserID);
        var eventid = await _context.Events.AnyAsync(e => e.Id == dto.EventID);

        if (!userid || !eventid)
        {
            return NotFound("the event or the user was not found");
        }
        var newAttendee = new Entities.Attendees
        {
            UserId = dto.UserID,
            EventId = dto.EventID,
           
        };
        
         await _context.Attendees.AddAsync(newAttendee);
        var saved=await _context.SaveChangesAsync();

        if (saved == 0)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetAttendeeById), new { id = newAttendee.Id }, newAttendee); ;
        
    }
    
    [HttpGet("{id}")]
    [EndpointName("Get Attendee By ID")]
    [EndpointDescription("Get an attendee by their unique ID")]
    [EndpointSummary("Returns a single attendee")]
    public async Task<IActionResult> GetAttendeeById(Guid id)
    {
        var attendee = await _context.Attendees
            .FirstOrDefaultAsync(a => a.Id == id);

        if (attendee == null)
        {
            return NotFound();
        }

        return Ok(attendee);
    }

    [HttpGet]
    [EndpointName("Get Attendees")]
    [EndpointDescription("Returns all attendees")]
    [EndpointSummary("Returns all attendees")]
    public async Task<ActionResult<List<Entities.Attendees>>> GetAttendees([FromQuery] Guid UserId)
    {
        var user=await _context.Users.AnyAsync(u => u.Id == UserId);
        if (!user)
        {
            return NotFound();
        }

        var allevents = await _context.Events.Where(u => u.Id == UserId).ToListAsync();
        if (allevents.Count == 0)
        {
            return NoContent();
        }

        return Ok(allevents);


    }

    [HttpDelete]
    [EndpointGroupName("Delete Attendee")]
    [EndpointDescription("Delete a attendee from an event")]
    [EndpointSummary("Deletes a attendee from an event")]
    public async Task<IActionResult> DeleteAttendee(Guid id)
    {
        var findAttendee= await _context.Attendees.FindAsync( id);
        if (findAttendee==null)
        {
            return NotFound();
        }

        return Ok("Deleted successfully");
    }

}