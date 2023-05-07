using vk_test_api.Data.Models.Base;
using vk_test_api.Database;
using vk_test_api.Data.Repositories.Interfaces;

namespace vk_test_api.Data.Repositories.Implimentations;

public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
{
    private UserContext Context { get; set; }
    public BaseRepository(UserContext context)
    {
        Context = context;
    }

    public TDbModel Create(TDbModel model)
    {
        Context.Set<TDbModel>().Add(model);
        Context.SaveChanges();
        return model;
    }

    public void Delete(Guid id)
    {
        var toDelete = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        Context.Set<TDbModel>().Remove(toDelete);
        Context.SaveChanges();
    }

    public List<TDbModel> GetAll()
    {
        return Context.Set<TDbModel>().ToList();
    }

    public TDbModel Update(TDbModel model)
    {
        var toUpdate = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
        if (toUpdate != null)
        {
            toUpdate = model;
        }
        Context.Update(toUpdate);
        Context.SaveChanges();
        return toUpdate;
    }

    public TDbModel Get(Guid id)
    {
        return Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
    }

    public IQueryable<TDbModel> Get()
    {
        return Context.Set<TDbModel>();
    }
}
