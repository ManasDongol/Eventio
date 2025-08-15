using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eventing.ApiService.Data.Entities;
using Eventing.ApiService.Data.Enums;

namespace Eventing.ApiService.Entities;

[Table(name: "attendees")]
public class Attendees
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    
    //navigation
    public Events? Event { get; set; } = null!; //null ignore operator
    public Users? User { get; set; } = null!;

    public RsvpResponse Response { get; set; } = RsvpResponse.Pending;
    public DateTime? RespondedAt { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; } = null!;
    
    [MaxLength(500)]
    public string? Comment { get; set; }
    public bool IsOrganizer { get; set; }
}