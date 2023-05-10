using Microsoft.EntityFrameworkCore;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Interfaces;

namespace vk_test_api.Data.Repositories.Implimentations;

public class UserStateRepository : BaseRepository<UserState>, IUserStateRepository
{
    public UserStateRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserState> GetState(string state)
    {
        return await _context.User_States.FirstAsync(u => u.Code == state);
    }

    private AppDbContext _context => (AppDbContext)Context;
}
