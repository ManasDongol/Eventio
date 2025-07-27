using Microsoft.EntityFrameworkCore;

namespace Eventing.ApiService.Data.Entities.Seeders;

public class EventSeeder
{

    public static async Task SeedAsync(DbContext context, CancellationToken cancellation)
    {
        var events = new List<Events>
        {
            new Events
            {
                Title = "Annual Meeting",
                CreatedBy = Guid.Parse("11111111-1111-1111-1111-111111111111"), // John Doe's Id
                Location = "Conference Hall A",
                EventDescription = "Annual company-wide meeting",
                EventType = "Meeting",
                StartTime = new DateTime(2025, 9, 15, 9, 0, 0,DateTimeKind.Utc),
                EndTime = new DateTime(2025, 9, 15, 12, 0, 0,DateTimeKind.Utc),
                CreatedTime = DateTime.Now
            },
            new Events
            {
                Title = "Tech Conference",
                CreatedBy = Guid.Parse("22222222-2222-2222-2222-222222222222"), // Alice Johnson's Id
                Location = "Tech Park Auditorium",
                EventDescription = "A conference on emerging tech trends",
                EventType = "Conference",
                StartTime = new DateTime(2025, 10, 10, 10, 0, 0,DateTimeKind.Utc),
                EndTime = new DateTime(2025, 10, 10, 18, 0, 0,DateTimeKind.Utc),
                CreatedTime = DateTime.Now
            },
            new Events
            {
                Title = "Marketing Workshop",
                CreatedBy = Guid.Parse("33333333-3333-3333-3333-333333333333"), // Michael Smith's Id
                Location = "Room 204",
                EventDescription = "Workshop on latest marketing strategies",
                EventType = "Workshop",
                StartTime = new DateTime(2025, 8, 5, 13, 0, 0,DateTimeKind.Utc),
                EndTime = new DateTime(2025, 8, 5, 17, 0, 0,DateTimeKind.Utc),
                CreatedTime = DateTime.Now
            },
            new Events
            {
                Title = "Product Launch",
                CreatedBy = Guid.Parse("44444444-4444-4444-4444-444444444444"), // Emily Brown's Id
                Location = "Main Auditorium",
                EventDescription = "Launch event for new product line",
                EventType = "Launch",
                StartTime = new DateTime(2025, 11, 20, 15, 0, 0,DateTimeKind.Utc),
                EndTime = new DateTime(2025, 11, 20, 18, 0, 0,DateTimeKind.Utc),
                CreatedTime = DateTime.Now
            },
            new Events
            {
                Title = "Team Building Retreat",
                CreatedBy = Guid.Parse("55555555-5555-5555-5555-555555555555"), // Robert Taylor's Id
                Location = "Lakeside Resort",
                EventDescription = "Outdoor activities and bonding",
                EventType = "Retreat",
                StartTime = new DateTime(2025, 7, 25, 8, 0, 0,DateTimeKind.Utc),
                EndTime = new DateTime(2025, 7, 27, 17, 0, 0,DateTimeKind.Utc),
                CreatedTime = DateTime.Now
            }
        };
    }
  

    
}