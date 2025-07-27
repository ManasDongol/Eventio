using System.ComponentModel;

namespace Eventing.ApiService.Controllers.User.Dto;

public sealed record UserResponse(
    [property: Description("The ID of the user")]
    Guid Id,
    [property: Description("The user's full name, up to 64 characters")]
    string Name,
    [property: Description("The user's email address")]
    string Email,
    [property: Description("The user's address, up to 128 characters")]
    string Address
)
{
    public static UserResponse From(Data.Entities.Users users)
        => new(users.Id, users.Name, users.Email, users.Address);
}