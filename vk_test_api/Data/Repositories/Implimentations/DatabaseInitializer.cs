using Microsoft.EntityFrameworkCore;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Interfaces;

namespace vk_test_api.Data.Repositories.Implimentations;

public class DatabaseInitializer : IDatabaseInitializer
{
    readonly AppDbContext _context;

    public DatabaseInitializer(AppDbContext context)
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
        if (!await _context.User_Groups.AnyAsync())
        {
            _context.User_Groups.Add(new UserGroup { Code = "Admin", Description = "This is default description" });
            _context.User_Groups.Add(new UserGroup { Code = "User", Description = "This is default description" });
        }
    }
    private async Task SeedDefaultStatesAsync()
    {
        if (!await _context.User_States.AnyAsync())
        {
            _context.User_States.Add(new UserState { Code = "Active", Description = "This is default description" });
            _context.User_States.Add(new UserState { Code = "Blocked", Description = "This is default description" });
        }
    }
}
