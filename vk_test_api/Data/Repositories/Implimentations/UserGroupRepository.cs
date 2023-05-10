using Microsoft.EntityFrameworkCore;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Interfaces;

namespace vk_test_api.Data.Repositories.Implimentations;

public class UserGroupRepository : BaseRepository<UserGroup>, IUserGroupRepository
{
    public UserGroupRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserGroup> GetUserGroup(string userGroup)
    {
        return await _context.User_Groups.FirstAsync(u => u.Code == userGroup);
    }

    private AppDbContext _context => (AppDbContext)Context;
}
