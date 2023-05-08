using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using vk_test_api.Core.Mapper;
using vk_test_api.Core.Services.Interfaces;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Interfaces;
using vk_test_api.Data.RequestObject;

namespace vk_test_api.Core.Services.Implimentations;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _usersRepository;
    private readonly IBaseRepository<UserGroup> _userGroupRepository;
    private readonly IBaseRepository<UserState> _usersStateRepository;

    public UserService(IBaseRepository<User> usersRepository,IBaseRepository<UserGroup> userGroupRepository, IBaseRepository<UserState> usersStateRepository)
    {
        _usersRepository = usersRepository;
        _userGroupRepository = userGroupRepository;
        _usersStateRepository = usersStateRepository;
    }

    public async Task<IEnumerable<UserState>> GetAllStates() => await _usersStateRepository.Query().ToListAsync();

    public IEnumerable<User> GetAll() => _usersRepository.Query()
        .Include(u => u.Group)
        .Include(u => u.State)
        .ToList();

    public async Task<User> Get(Guid id) => await _usersRepository.Query()
        .Include(u => u.Group)
        .Include(u => u.State)
        .FirstOrDefaultAsync(u => u.Id == id);

    public (bool, User?) Add(PostUserObject user)
    {
        var userEntity = UserExtensions.ToUser(user);

        //IsAdminOnBoard
        //{
        var adminRoleId = _userGroupRepository.Query().First(u => u.Code == "Admin").Id;
        bool isAdminEntity = _usersRepository.Query().Any(u => u.UserGroupId == adminRoleId);

        if (isAdminEntity && userEntity.UserGroupId == adminRoleId)
        {
            return (false, null); 
        }
        //}
        userEntity.DateCreated = DateTime.UtcNow;
        userEntity.UserStateId = _usersStateRepository.Query().First(u => u.Code == "Active").Id;

        return (true, userEntity);
    }

    public void Update(User user)
    {
        _usersRepository.Update(user);
    }

    public void Delete(Guid id)
    {
        var userEntity = _usersRepository.Query().First(u => u.Id == id);

        userEntity.UserStateId = _usersStateRepository.Query().First(u => u.Code == "Blocked").Id;
        Update(userEntity);
    }
}