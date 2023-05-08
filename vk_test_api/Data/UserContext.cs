using Microsoft.EntityFrameworkCore;
using vk_test_api.Data.Models;

namespace vk_test_api.Database;

public class UserContext : DbContext
{
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserState> UserStates { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }
}