using Microsoft.EntityFrameworkCore;
using vk_test_api.Data.Repositories.Interfaces;
using vk_test_api.Data.Models;
namespace vk_test_api.Data.Repositories.Implimentations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<User>> GetAll()
    {
        return await _context.Users
            .Include(u => u.Group)
            .Include(u => u.State)
            .ToListAsync();
    }

    public async Task<User> Get(Guid id)
    {
        return await _context.Users
            .Include(u => u.Group)
            .Include(u => u.State)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<User> Create(User model)
    {
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<User> Update(User model)
    {
        var toUpdate = await _context.Users.FirstOrDefaultAsync(m => m.Id == model.Id);
        if (toUpdate != null)
        {
            toUpdate = model;
        }

        _context.Update(toUpdate);
        await _context.SaveChangesAsync();
        return toUpdate;
    }

    public async Task Delete(Guid id)
    {
        var toDelete = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
        _context.Users.Remove(toDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsAdminExist()
    {
        return _context.Users.Any(u => u.Group.Code == "Admin");
    }
    public async Task<User> GetUser(string username, string password)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Login == username && x.Password == password);
    }

    public async Task<UserPaginatedResponse> GetUsers(UserParametrs userParameters)
    {
        return new UserPaginatedResponse()
        {
            TotalCount = await _context.Users.CountAsync(),
            Users = await _context.Users
            .OrderBy(on => on.Login)
            .Skip((userParameters.PageNumber - 1) * userParameters.PageSize)
            .Take(userParameters.PageSize)
            .Include(u => u.Group)
            .Include(u => u.State)
            .ToListAsync()
        };
    }

    private AppDbContext _context => (AppDbContext)Context;
}
