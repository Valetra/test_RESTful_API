using vk_test_api.Data.Models;

namespace vk_test_api.Data.Repositories.Interfaces;

public interface IUserGroupRepository : IBaseRepository<UserGroup>
{
    Task<UserGroup> GetUserGroup(string userGroup);
}
