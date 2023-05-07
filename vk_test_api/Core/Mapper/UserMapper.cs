using vk_test_api.Data.Models;
using vk_test_api.Data.RequestObject;
using vk_test_api.Data.Models;

namespace vk_test_api.Core.Mapper;

public static class UserExtensions
{
    public static User ToUser(this PostUserObject newUser) => new User
    {
        Login = $"{newUser.Login}",

        PasswordHash = $"{newUser.PasswordHash}",

        Group = new UserGroup { Code = newUser.GroupCode, Description = "default" },

        State = new UserState { Code = newUser.StateCode, Description = "default" }
    };
}