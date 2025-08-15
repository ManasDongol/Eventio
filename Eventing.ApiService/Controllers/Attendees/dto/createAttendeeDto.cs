using System.ComponentModel.DataAnnotations;

namespace Eventing.ApiService.Controllers.Attendees.dto;

public sealed record createAttendeeDto(
    
    
    [Required]
    Guid UserID,
    
    [Required]
    Guid EventID,
    
    
    bool IsOrganizer=false,
    string comment="No comment"
    
    );
