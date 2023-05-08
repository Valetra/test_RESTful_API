namespace vk_test_api.Data.Repositories.Interfaces;

public interface IDatabaseInitializer
{
    Task SeedAsync();
}
