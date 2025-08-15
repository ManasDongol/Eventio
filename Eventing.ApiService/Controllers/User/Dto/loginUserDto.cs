using System.ComponentModel.DataAnnotations;


namespace Eventing.ApiService.Controllers.User.Dto;

public sealed record loginUserDto
(
    [Required]
string Username,
[Required]
string Password
    );