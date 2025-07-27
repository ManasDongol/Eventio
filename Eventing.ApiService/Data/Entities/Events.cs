using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eventing.ApiService.Data.Entities;
using Eventing.ApiService.Entities;

namespace Eventing.ApiService.Data.Entities;


[Table(name: "events")]
public class Events
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid CreatedBy { get; set; }
    
    [MaxLength(120)]
    public string Title {get; set;}
    
    [MaxLength(256)]
    [Column("location")]
    public string Location {get; set;}
    
    [MaxLength(120)]
    public string EventType {get; set;}
    
    [Column("starttime")]
    public DateTime? StartTime {get; set;}
    
    [Column("endtime")]
    public DateTime? EndTime {get; set;}
    
    [Column("createdtime")]
    public DateTime? CreatedTime {get; set;}= DateTime.UtcNow;
    
    [MaxLength(500)]
    public string? EventDescription {get; set;}
    
    [ForeignKey(nameof(CreatedBy))]
    //nav property UserID - ForeignKey
    public Users  CreatedByUsers {get; set;}
}