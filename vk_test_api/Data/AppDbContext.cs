using Microsoft.EntityFrameworkCore;
using vk_test_api.Data.Models;

namespace vk_test_api.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserState> UserStates { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}