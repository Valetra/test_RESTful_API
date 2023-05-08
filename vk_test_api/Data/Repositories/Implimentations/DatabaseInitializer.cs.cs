using Microsoft.EntityFrameworkCore;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Interfaces;
using vk_test_api.Database;

namespace vk_test_api.Data.Repositories.Implimentations;

public class DatabaseInitializer : IDatabaseInitializer
{
    readonly UserContext _context;

    public DatabaseInitializer(UserContext context)
    {
        _context = context;
    }
    public async Task SeedAsync()
    {
        await _context.Database.MigrateAsync();

        await SeedDefaultGroupsAsync();
        await SeedDefaultStatesAsync();

        await _context.SaveChangesAsync();
    }
    private async Task SeedDefaultGroupsAsync()
    {
        if (!await _context.UserGroups.AnyAsync())
        {
            _context.UserGroups.Add(new UserGroup { Code = "Admin", Description = "Who is your daddy?!" });
            _context.UserGroups.Add(new UserGroup { Code = "User", Description = "Who is my daddy?" });
        }
    }
    private async Task SeedDefaultStatesAsync()
    {
        if (!await _context.UserStates.AnyAsync())
        {
            _context.UserStates.Add(new UserState { Code = "Active", Description = "Where do you prefer more: from the top/bottom?" });
            _context.UserStates.Add(new UserState { Code = "Blocked", Description = "I`d like to on the bottom!" });
        }
    }
}
