using System.ComponentModel;
using Eventing.ApiService.Controllers.User.Dto;
using Microsoft.AspNetCore.Mvc;
using Eventing.ApiService.Data;
using Eventing.ApiService.Data;
using Eventing.ApiService.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eventing.ApiService.Controllers.User;

[ApiConventionType(typeof(DefaultApiConventions))]
public sealed class UsersController : ApiBaseController
{
    private readonly EventingDbContext _context;
    private readonly UserManager<Users> _userManager;

    
   public UsersController(EventingDbContext context,UserManager<Users> userManager)
   {
       _context = context;
       _userManager = userManager;
   }

    [HttpGet]
    [EndpointName("GetAllUsers")]
    [EndpointSummary("Get all users")]
    [EndpointDescription("Returns a list of all users.")]
    public async Task<ActionResult<List<UserResponse>>> GetAll() { 
        var user= await _context.Users.ToListAsync();
        if (user == null || user.Count == 0)
            return NotFound();
    
        // Map each User entity to UserResponse
          var userResponses = user.Select(u => UserResponse.From(u)).ToList();
      
          return Ok(userResponses);
      }

    [HttpGet("{id:guid}")]
    [EndpointName("GetUserById")]
    [EndpointSummary("Get user by ID")]
    [EndpointDescription("Returns a single user by their unique identifier.")]
    public async Task<ActionResult<UserResponse>> GetById([FromRoute, Description("The ID of the user to retrieve")] Guid id)
    {
        // Retrieve the user from the database asynchronously
        var user = await _context.Users.FindAsync(id);
    
        if (user == null) 
            return NotFound();
    
        return Ok(UserResponse.From(user));
    }

    [HttpPost]
    [EndpointName("CreateUser")]
    [EndpointSummary("Create a new user")]
    [EndpointDescription("Creates a user and returns the location of the newly created resource.")]
    public async Task<IActionResult> Create([FromBody, Description("The user details to create")] CreateUserRequestDto dto)
    {
        var user = new Data.Entities.Users()
        {
            Id = Guid.NewGuid(),
            
            UserName = dto.Username,

            Name = dto.Name,
            Email = dto.Email,
            Address = dto.Address,
        };
        
        var result=await _userManager.CreateAsync(user,dto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
       


        return CreatedAtAction(nameof(GetById), new { id = user.Id }, null);
    }

    [HttpPut("{id:guid}")]
    [EndpointName("UpdateUser")]
    [EndpointSummary("Update user")]
    [EndpointDescription("Updates the details of an existing user.")]
    public async Task<IActionResult> Update(
        [FromRoute, Description("The ID of the user to update")]
        Guid id,
        [FromBody, Description("The updated user data")]
        UpdateUserRequestDto dto)
    {
        var user = await  _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        user.Name = dto.Name;
        user.Email = dto.Email;
        user.Address = dto.Address;
        

      await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [EndpointName("DeleteUser")]
    [EndpointSummary("Delete user")]
    [EndpointDescription("Removes a user by their unique identifier.")]
       
       public async Task<IActionResult> DeleteUser(Guid id)
       {
           var user = await _context.Users.FindAsync(id);
       
           if (user == null)
               return NotFound();
       
           _context.Users.Remove(user);
           await _context.SaveChangesAsync();
       
           return NoContent(); // HTTP 204
       }

}