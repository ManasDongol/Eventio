using System.ComponentModel;

namespace Eventing.ApiService.Controllers.Events.Dto;

public sealed record EventResponse(
    
    [property: Description("The user's full name, up to 64 characters")]
    string Title,
    [property: Description("The user's email address")]
    string Location,
    [property: Description("The user's address, up to 128 characters")]
    string EventType,
    [property: Description("The user's address, up to 128 characters")]
    DateTime? StartTime,
    [property: Description("The user's address, up to 128 characters")]
    DateTime? EndTime,
    [property: Description("The user's address, up to 128 characters")]
    DateTime? CreatedTime,
    [property: Description("The user's address, up to 128 characters")]
    string EventDescription
 
)

{
public static EventResponse From(Data.Entities.Events events) => new(events.Title, events.Location, events.EventType, events.StartTime, events.EndTime, events.CreatedTime,events.EventDescription);
}