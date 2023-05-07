namespace vk_test_api.Data.RequestObject;

public class PostUserObject
{
    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public string GroupCode { get; set; }
    public string StateCode { get; set; }
}