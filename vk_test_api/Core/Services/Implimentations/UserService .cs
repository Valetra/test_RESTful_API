using Microsoft.EntityFrameworkCore;
using vk_test_api.Core.Mapper;
using vk_test_api.Core.Services.Interfaces;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Interfaces;
using vk_test_api.Data.RequestObject;
using vk_test_api.Core.Exceptions;

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

    public async Task<IEnumerable<User>> GetAll() => await _usersRepository.Query()
        .Include(u => u.Group)
        .Include(u => u.State)
        .ToListAsync();

    public async Task<User> Get(Guid id) => await _usersRepository.Query()
        .Include(u => u.Group)
        .Include(u => u.State)
        .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User> Add(PostUserObject user)
    {
        var userEntity = UserExtensions.ToUser(user);

        if (user.GroupCode == "Admin")
        {
            bool hasAnyAdmin = await _usersRepository.Query().AnyAsync(u => u.Group.Code == "Admin");

            if (hasAnyAdmin)
            {
                throw new AdminAlreadyExistsException();
            }
        }

        userEntity.DateCreated = DateTime.UtcNow;
        userEntity.State = await _usersStateRepository.Query().FirstAsync(u => u.Code == "Active");
        userEntity.Group = await _userGroupRepository.Query().FirstAsync(u => u.Code == user.GroupCode);
        //FiveSecounsTimer

        return await _usersRepository.Create(userEntity);
    }

    public async Task<User> Update(User user)
    {
        return await _usersRepository.Update(user);
    }

    public async Task Delete(Guid id)
    {
        var userEntity = await _usersRepository.Query().FirstAsync(u => u.Id == id);

        userEntity.State = await _usersStateRepository.Query().FirstAsync(u => u.Code == "Blocked");
        await Update(userEntity);
    }
}