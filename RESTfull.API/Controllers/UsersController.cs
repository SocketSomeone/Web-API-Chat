﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTfull.API.Models;
using RESTfull.Domain;
using RESTfull.Infrastructure.Repositories;

namespace RESTfull.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public UsersController(IRepository<User> userRepository) => _userRepository = userRepository;

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = _userRepository.Get(u => u.Username == model.Username);

        if (user != null)
            return BadRequest("Username already in use");

        _userRepository.Add(new User()
        {
            Username = model.Username,
            Password = model.Password
        });
        return Ok();
    }

    [Authorize]
    [HttpGet("@me")]
    public async Task<IActionResult> Get()
    {
        return Ok(_userRepository.Get(u => u.Username == User.Identity.Name));
    }
}