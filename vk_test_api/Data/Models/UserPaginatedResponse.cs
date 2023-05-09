namespace vk_test_api.Data.Models;

public class UserPaginatedResponse
{
    public List<User> Users { get; set; }
    public int TotalCount { get; set; }
}