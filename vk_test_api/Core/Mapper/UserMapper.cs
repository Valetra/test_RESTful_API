using System.Text;
using vk_test_api.Data.Models;
using vk_test_api.Data.RequestObject;

namespace vk_test_api.Core.Mapper;

public static class UserExtensions
{
    public static User ToUser(this PostUserObject newUser) => new User
    {
        Login = newUser.Login,

        PasswordHash = ConvertPasswordToBase64(newUser.Password)
    };

    public static string ConvertPasswordToBase64(string password)
    {
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(password));
    }
}