using vk_test_api.Data.Models;
using vk_test_api.Data.RequestObject;
using vk_test_api.Data.Models;
using System.Text.RegularExpressions;

namespace vk_test_api.Core.Mapper;

public static class UserExtensions
{
    public static User ToUser(this PostUserObject newUser) => new User
    {
        Login = newUser.Login,

        Password = newUser.Password
    };
}