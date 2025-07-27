using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eventing.ApiService.Controllers.Events.Dto;

public sealed record CreateEventRequestDto(
    [Required]
    [MaxLength(64)]
    [RegularExpression("^([A-Z][a-z]+)( [A-Z][a-z]+)*$",ErrorMessage = "The Full Name field is not in a valid format.")]
    [property: Description("The user's full name, up to 64 characters")]
    string Title,

    
    [Required]
    [property: Description("The user's  ID")]
    Guid CreatedBy,

    [Required]
    [property: Description("The user's email address")]
    string Location,

    [Required]
    [MaxLength(128)]
    [property: Description("The user's address, up to 128 characters")]
    string EventDescription,
    
    [Required]
    [MaxLength(128)]
    [property: Description("The user's address, up to 128 characters")]
    string EventType,
    
    [Required]
    [property: Description("The user's address, up to 128 characters")]
    DateTime StartTime,
    
    [Required]
    [property: Description("The user's address, up to 128 characters")]
    DateTime EndtTime,
    
    [Required]
    [property: Description("The user's address, up to 128 characters")]
    DateTime CreatedTime
    

)
{
    
}