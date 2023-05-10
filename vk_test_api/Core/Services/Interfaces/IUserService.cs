using vk_test_api.Data.Models;
using vk_test_api.Data.RequestObject;

namespace vk_test_api.Core.Services.Interfaces;
public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User> Get(Guid id);
    Task<User> Add(PostUserObject user);
    Task<User> Update(User user);
    Task Delete(Guid id);
    Task<User> Authenticate(string username, string password);
    Task<UserPaginatedResponse> GetUsers(UserParametrs userParameters);
}