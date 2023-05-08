using vk_test_api.Data.Models.Base;
using vk_test_api.Database;
using vk_test_api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace vk_test_api.Data.Repositories.Implimentations;

public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
{
    private UserContext Context { get; set; }
    public BaseRepository(UserContext context)
    {
        Context = context;
    }

    public async Task<TDbModel> Create(TDbModel model)
    {
        await Context.Set<TDbModel>().AddAsync(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task Delete(Guid id)
    {
        var toDelete = await Query().FirstOrDefaultAsync(m => m.Id == id);
        Context.Set<TDbModel>().Remove(toDelete);
        await Context.SaveChangesAsync();
    }

    public async Task<List<TDbModel>> GetAll()
    {
        return await Query().ToListAsync();
    }

    public async Task<TDbModel> Update(TDbModel model)
    {
        var toUpdate = await Query().FirstOrDefaultAsync(m => m.Id == model.Id);
        if (toUpdate != null)
        {
            toUpdate = model;
        }

        Context.Update(toUpdate);
        await Context.SaveChangesAsync();
        return toUpdate;
    }

    public async Task<TDbModel> Get(Guid id)
    {
        return await Query().FirstOrDefaultAsync(m => m.Id == id);
    }

    public IQueryable<TDbModel> Query()
    {
        return Context.Set<TDbModel>();
    }
}