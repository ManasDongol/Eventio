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

    [HttpPost("register")]
    public async Task<IActionResult> registerUser([FromBody] CreateUserRequestDto dto)
    {
      
        var existingUser = await _userManager.FindByNameAsync(dto.Username);
        if (existingUser != null)
        {
            Console.WriteLine($"Username {dto.Username} already exists");
            return BadRequest("Username already exists");
        }

        
        var existingEmailUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingEmailUser != null)
        {
            Console.WriteLine($"Email {dto.Email} already exists");
            return BadRequest("Email already exists");
        }

        // Validate email format
        if (string.IsNullOrEmpty(dto.Email) || !IsValidEmail(dto.Email))
        {
            Console.WriteLine("Invalid email format");
            return BadRequest("Invalid email format");
        }

        // Validate required fields
        if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password) || 
            string.IsNullOrEmpty(dto.Name))
        {
            Console.WriteLine("Missing required fields");
            return BadRequest("Username, name, and password are required");
        }

        // Create new user
        var newUser = new Users
        {
            UserName = dto.Username,
            Email = dto.Email,
            Name = dto.Name,
            Address = dto.Address 
        };

        // Create user with password
        var result = await _userManager.CreateAsync(newUser, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            Console.WriteLine($"Failed to create user: {errors}");
            return BadRequest($"Failed to create user: {errors}");
        }

        Console.WriteLine($"User {dto.Username} registered successfully!");
        return Ok("User registered successfully");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}