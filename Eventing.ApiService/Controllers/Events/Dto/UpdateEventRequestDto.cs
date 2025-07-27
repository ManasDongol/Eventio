using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eventing.ApiService.Controllers.Events.Dto;

public sealed record UpdateEventRequestDto_
(
    [Required]
    [MaxLength(64)]
    [RegularExpression("^([A-Z][a-z]+)( [A-Z][a-z]+)*$",ErrorMessage = "The Full Name field is not in a valid format.")]
    [property: Description("The event title")]
    string Title,
        
    [Required]
    [property: Description("The event location")]
    string Location,

    [Required]
    [MaxLength(128)]
    [property: Description("The event description")]
    string EventDescription,
    
    [Required]
    [MaxLength(128)]
    [property: Description("The event type")]
    string EventType,
    
    [Required]
    [property: Description("The event starttime")]
    DateTime StartTime,
    
    [Required]
    [property: Description("The event endtime")]
    DateTime EndtTime
    
    
    );