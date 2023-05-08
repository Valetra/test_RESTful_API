using vk_test_api.Data.Models;
using vk_test_api.Data.RequestObject;

namespace vk_test_api.Core.Services.Interfaces;
public interface IUserService
{
    IEnumerable<User> GetAll();
    Task<User> Get(Guid id);

    User Add(PostUserObject user);

    void Delete(Guid id);
}