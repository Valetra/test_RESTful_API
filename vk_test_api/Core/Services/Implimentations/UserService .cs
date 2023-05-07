using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using vk_test_api.Core.Services.Interfaces;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Interfaces;

namespace vk_test_api.Core.Services.Implimentations;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _users;
    //private readonly IBaseRepository<UserGroup> _userGroups;
    //private readonly IBaseRepository<UserState> _userStates;

    public UserService(IBaseRepository<User> users)//IBaseRepository<UserGroup> userGroups, IBaseRepository<UserState> userStates
    {
        _users = users;
        //_userGroups = userGroups;
        //_userStates = userStates;
    }

    public IEnumerable<User> GetAll()
    {
        return _users.GetAll();
    }

    public User Get(Guid id) => _users.Get()
        .Include(u => u.Group)
        .Include(u => u.State)
        .FirstOrDefault(u => u.Id == id);


    public User Add(User user)
    {
        user.DateCreated = DateTime.UtcNow;
        user.State.Code = "Active";
        user.State.Description = "description that you deserve";

        return _users.Create(user);
    }
    public void Update(User user)
    {
        _users.Update(user);
    }
    public void Delete(Guid id)
    {
        _users.Delete(id);
    }
}