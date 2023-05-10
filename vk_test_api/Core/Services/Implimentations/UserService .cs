using Microsoft.EntityFrameworkCore;
using vk_test_api.Core.Mapper;
using vk_test_api.Core.Services.Interfaces;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Interfaces;
using vk_test_api.Data.RequestObject;
using vk_test_api.Core.Exceptions;
using System.Buffers.Text;
using System.Text;

namespace vk_test_api.Core.Services.Implimentations;
public class UserService : IUserService
{
    private readonly IUserRepository _usersRepository;
    private readonly IUserStateRepository _userStateRepository;
    private readonly IUserGroupRepository _userGroupRepository;

    private static readonly ISet<string> creatingLogins = new HashSet<string>();
    public UserService(IUserRepository usersRepository, IUserStateRepository userStateRepository, IUserGroupRepository userGroupRepository)
    {
        _usersRepository = usersRepository;
        _userStateRepository = userStateRepository;
        _userGroupRepository = userGroupRepository;
    }

    public async Task<IEnumerable<User>> GetAll() => await _usersRepository.GetAll();
    public async Task<User> Get(Guid id) => await _usersRepository.Get(id);

    public async Task<User> Add(PostUserObject user)
    {
        if (creatingLogins.Contains(user.Login))
            throw new UserIsCreatingException();

        creatingLogins.Add(user.Login);

        var userEntity = UserExtensions.ToUser(user);

        if (user.GroupCode == "Admin")
        {
            if (await _usersRepository.IsAdminExist())
            {
                throw new AdminAlreadyExistsException();
            }
        }

        userEntity.DateCreated = DateTime.UtcNow;
        userEntity.State = await _userStateRepository.GetState("Active");
        userEntity.Group = await _userGroupRepository.GetUserGroup(user.GroupCode);

        await Task.Delay(TimeSpan.FromSeconds(5));
        creatingLogins.Remove(user.Login);

        try
        {
            return await _usersRepository.Create(userEntity);
        }
        catch (DbUpdateException)
        {
            throw new LoginExistsException();
        }
    }

    public async Task<User> Update(User user)
    {
        return await _usersRepository.Update(user);
    }

    public async Task Delete(Guid id)
    {
        var userEntity = await _usersRepository.Get(id);

        userEntity.State = await _userStateRepository.GetState("Blocked");
        await Update(userEntity);
    }

    public async Task<User> Authenticate(string username, string password)
    {
        return await _usersRepository.GetUser(username, password);
    }

    public async Task<UserPaginatedResponse> GetUsers(UserParametrs userParameters)
    {
        return await _usersRepository.GetUsers(userParameters);
    }
}