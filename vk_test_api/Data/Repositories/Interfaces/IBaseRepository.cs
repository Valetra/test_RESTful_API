using vk_test_api.Data.Models;
using vk_test_api.Data.Models.Base;

namespace vk_test_api.Data.Repositories.Interfaces;

public interface IBaseRepository<TDbModel> where TDbModel : BaseModel
{
    Task<List<TDbModel>> GetAll();
    Task<TDbModel> Get(Guid id);
    IQueryable<TDbModel> Query();
    Task<TDbModel> Create(TDbModel model);
    Task<TDbModel> Update(TDbModel model);
    Task Delete(Guid id);
}