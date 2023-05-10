using vk_test_api.Data.Models;

namespace vk_test_api.Data.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> IsAdminExist();
    Task<User> GetUser(string username, string password);
    Task<UserPaginatedResponse> GetUsers(UserParametrs userParameters);
}