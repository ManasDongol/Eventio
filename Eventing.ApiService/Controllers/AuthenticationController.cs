using Eventing.ApiService.Controllers.User.Dto;
using Eventing.ApiService.Data.Entities;
using Eventing.ApiService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventing.ApiService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController: ApiBaseController
{

    private readonly EventingDbContext _context;

    private readonly UserManager<Users> _userManager;

    private readonly JwtTokenService _tokenService;

   
    public AuthenticationController(EventingDbContext context,UserManager<Users> userManager,JwtTokenService tokenService)
    {
        _context = context;
        _userManager = userManager;
        _tokenService = tokenService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> authenticateUserLogin([FromBody] loginUserDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user == null)
        {
            
            Console.WriteLine("User doesnt exist! please try again later");
            return Unauthorized("Invalid credentials");
        
        }

        var verify = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!verify)
        {
            Console.WriteLine("Wrong password");
            return Unauthorized("invalid credentials");
        }
        
        string token= _tokenService.GenerateJwtToken(user.Id, dto.Username);
        
        
        Console.WriteLine("Logged in!");
        return Ok("succesful login");
    }
}