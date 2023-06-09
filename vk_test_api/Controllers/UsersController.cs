﻿using Microsoft.AspNetCore.Mvc;
using vk_test_api.Core.Services.Interfaces;
using vk_test_api.Data.Models;
using vk_test_api.Data.RequestObject;
using vk_test_api.Core.Exceptions;
using vk_test_api.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace vk_test_api.Controllers;

[UserAuthorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    //For pagination use this: https://localhost/users?pageNumber=x&pageSize=y 
    public async Task<ActionResult<UserPaginatedResponse>> GetAll([FromQuery] UserParametrs userParametrs)
    {
        return Ok(await _userService.GetUsers(userParametrs));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await _userService.Get(id);
        return (user == null) ? Ok(user) : NotFound();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(PostUserObject user)
    {
        User newUser;

        try
        {
            newUser = await _userService.Add(user);
        }
        catch (AdminAlreadyExistsException)
        {
            return BadRequest("Service is already has an Admin.");
        }
        catch (UserIsCreatingException)
        {
            return BadRequest("User with this login is creating right now.");
        }
        catch (LoginExistsException)
        {
            return BadRequest("User with this login is already exicts.");
        }

        return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.Delete(id);
        return NoContent();
    }
}