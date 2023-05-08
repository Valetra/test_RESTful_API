using vk_test_api.Data.Models;
using vk_test_api.Data.Models.Base;
using vk_test_api.Data.Repositories.Implimentations;

namespace vk_test_api.Data.Repositories.Interfaces;

public interface IBaseRepository<TDbModel> where TDbModel : BaseModel
{
    List<TDbModel> GetAll();
    TDbModel Get(Guid id);
    IQueryable<TDbModel> Query();
    TDbModel Create(TDbModel model);
    TDbModel Update(TDbModel model);
    void Delete(Guid id);
}