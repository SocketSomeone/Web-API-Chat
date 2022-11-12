using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RESTfull.API.Models;
using RESTfull.Domain;
using RESTfull.Infrastructure;

namespace RESTfull.API.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context) => _context = context;
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] AuthModels.RegisterModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

        if (user != null)
            return BadRequest("Username already in use");

        await _context.Users.AddAsync(new User()
        {
            Username = model.Username,
            Password = model.Password
        });

        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [Authorize]
    [HttpGet("@me")]
    public async Task<IActionResult> Get()
    {
        return Ok(_context.Users.FirstOrDefault(u => u.Username == User.Identity.Name));
    }
}