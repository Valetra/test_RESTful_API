using vk_test_api.Data.Models;

namespace vk_test_api.Core.Services.Interfaces;
public interface IUserService
{
    public IEnumerable<User> GetAll();
    public User Get(Guid id);

    public User Add(User user);

    public void Delete(Guid id);
}