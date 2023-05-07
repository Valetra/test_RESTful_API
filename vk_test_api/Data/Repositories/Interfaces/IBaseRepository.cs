using vk_test_api.Data.Models.Base;

namespace vk_test_api.Data.Repositories.Interfaces;

public interface IBaseRepository<TDbModel> where TDbModel : BaseModel
{
    public List<TDbModel> GetAll();
    public TDbModel Get(Guid id);
    public IQueryable<TDbModel> Get();
    public TDbModel Create(TDbModel model);
    public TDbModel Update(TDbModel model);
    public void Delete(Guid id);
}
