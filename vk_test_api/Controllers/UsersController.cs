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
    [Route("states")]
    public async Task<ActionResult<IEnumerable<UserState>>> GetAllStates()
    {
        var result = await _userService.GetAllStates();
        return Ok(result);
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(Guid id)
    {
        var user = _userService.Get(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> PostUser(PostUserObject user)
    {
        var createdUser = _userService.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        _userService.Delete(id);
        return NoContent();
    }
}