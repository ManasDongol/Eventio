using System.ComponentModel;
using Eventing.ApiService.Controllers.Events.Dto;
using Eventing.ApiService.Controllers.User.Dto;
using Microsoft.AspNetCore.Mvc;
using Eventing.ApiService.Data;
using Eventing.ApiService.Data;
using Eventing.ApiService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eventing.ApiService.Controllers.Events;

[ApiConventionType(typeof(DefaultApiConventions))]
public sealed class EventController : ApiBaseController
{
    private readonly EventingDbContext _context;
   
    public EventController(EventingDbContext context)
    {
        _context = context;
    }
    
    //now for getting all Events

    [HttpGet]
    [EndpointName("GetEvents")]
    [EndpointDescription("gets all events")]
    [EndpointSummary("gets all events")]
    public async Task<ActionResult<List<EventResponse>>> GetEvents()
    {
        var _event=await _context.Events.ToListAsync();

        if (_event == null)
        {
            return NotFound();
        }

        var evenResponses = _event.Select(t=>EventResponse.From(t)).ToList();
        
        return Ok(evenResponses);
    }

    [HttpGet]
    [EndpointName("GetEventById")]
    [EndpointDescription("gets event by id")]
    [EndpointSummary("gets event by id")]
    public async Task<ActionResult> GetById([FromRoute, Description("The ID of the user to retrieve")] Guid id)
    {
        // Retrieve the user from the database asynchronously
        var _event = await _context.Events.FindAsync(id);


        if (_event == null)
        {
            return NotFound();
        }
        return Ok(EventResponse.From(_event));
    }

    [HttpPost]
    [EndpointName("CreateEvent")]
    [EndpointDescription("Creates a new event")]
    [EndpointSummary("creates a new event")]
    public IActionResult Create([FromBody, Description("The event details to create")] CreateEventRequestDto dto)
    {
       
        

        var _event = new Data.Entities.Events()
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            CreatedBy = dto.CreatedBy,
            Location=dto.Location,
            EventDescription = dto.EventDescription,
            EventType = dto.EventType,
            StartTime = ToUtc(dto.StartTime),
            EndTime = ToUtc(dto.EndtTime),
            CreatedTime = ToUtc(dto.CreatedTime)
        };

        _context.Events.Add(_event);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = _event.Id }, null);
    }
    
    //helper method to convert datetimes
    private DateTime ToUtc(DateTime dateTime)
    {
        return dateTime.Kind switch
        {
            DateTimeKind.Utc => dateTime,
            DateTimeKind.Local => dateTime.ToUniversalTime(),
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc),
            _ => dateTime
        };
    }


    
}