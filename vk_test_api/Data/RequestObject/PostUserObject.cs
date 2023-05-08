namespace vk_test_api.Data.RequestObject;

public class PostUserObject
{
    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public Guid GroupId { get; set; }
}