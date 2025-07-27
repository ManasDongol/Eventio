using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eventing.ApiService.Controllers.User.Dto;

public sealed record CreateUserRequestDto(
    
    [Required]
    [MaxLength(50)]
    [RegularExpression("^[a-zA-Z0-9]+$")]
    [property: Description("the users username")]
    string Username,
    
    [Required]
    [MaxLength(64)]
    [RegularExpression("^([A-Z][a-z]+)( [A-Z][a-z]+)*$", ErrorMessage = "The Full Name field is not in a valid format.")]
    [property: Description("The user's full name, up to 64 characters")]
    string Name,

    [Required]
    [EmailAddress]
    [property: Description("The user's email address")]
    string Email,

    [Required]
    [MaxLength(128)]
    [property: Description("The user's address, up to 128 characters")]
    string Address,
    
    [Required]
    [MaxLength(128)]
    [property:Description("the users password")]
    string Password
);