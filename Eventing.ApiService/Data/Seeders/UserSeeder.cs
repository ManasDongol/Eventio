using Microsoft.EntityFrameworkCore;

namespace Eventing.ApiService.Data.Entities.Seeders;

public static class UserSeeder
{
    public static async Task SeedAsync(DbContext context, CancellationToken cancellation)
    {
        var users = new List<Users>
        {
            new Users
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "John Doe",
                Email = "john.doe@example.com",
                Address = "123 Main Street, Springfield"
            },
            new Users
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Alice Johnson",
                Email = "alice.johnson@example.com",
                Address = "456 Elm Avenue, Metropolis"
            },
            new Users
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Michael Smith",
                Email = "michael.smith@example.com",
                Address = "789 Oak Drive, Gotham City"
            },
            new Users
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Emily Brown",
                Email = "emily.brown@example.org",
                Address = "321 Birch Lane, Star City"
            },
            new Users
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Name = "Robert Taylor",
                Email = "robert.taylor@example.net",
                Address = "654 Maple Street, Central City"
            }
        };

        var existingId = await context.Set<Users>()
            .Where(x => users.Select(x=>x.Id).Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellation);

        var NonExistingUsers = users.ExceptBy(existingId, x => x.Id).ToArray();
        
        context.Set<Users>().AddRange(NonExistingUsers);
        await context.SaveChangesAsync(cancellation);

    }
    
}