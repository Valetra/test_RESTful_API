using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using vk_test_api.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Routing;
using vk_test_api.Core.Services.Interfaces;
using vk_test_api.Core.Mapper;
using vk_test_api.Data.Models;
using vk_test_api.Data.RequestObject;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using vk_test_api.Core.Exceptions;

namespace vk_test_api.Controllers;

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
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        return Ok(await _userService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await _userService.Get(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

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
            return BadRequest("Service is already has an Admin!");
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