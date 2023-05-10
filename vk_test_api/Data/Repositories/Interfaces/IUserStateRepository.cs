using vk_test_api.Data.Models;

namespace vk_test_api.Data.Repositories.Interfaces;

public interface IUserStateRepository : IBaseRepository<UserState>
{
    Task<UserState> GetState(string state);
}
