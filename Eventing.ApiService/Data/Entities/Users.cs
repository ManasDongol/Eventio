using System.ComponentModel.DataAnnotations;
using Eventing.ApiService.Entities;
using Microsoft.AspNetCore.Identity;

namespace Eventing.ApiService.Data.Entities;

public class Users : IdentityUser<Guid>  //GUid primary key
{
 
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string Address { get; set; }
    
    // Other properties
    public ICollection<Attendees> Attendees { get; set; }=new  List<Attendees>();
    public ICollection<Events> Events { get; set; }=new  List<Events>();
}