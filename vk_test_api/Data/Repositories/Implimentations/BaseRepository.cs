using vk_test_api.Data.Models.Base;
using vk_test_api.Data;
using vk_test_api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using vk_test_api.Data.Models;

namespace vk_test_api.Data.Repositories.Implimentations;

public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
{
    protected AppDbContext Context { get; set; }
    protected readonly DbSet<TDbModel> _entities;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
        _entities = Context.Set<TDbModel>();
    }

    public async Task<List<TDbModel>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public async Task<TDbModel> Get(Guid id)
    {
        return await _entities.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<TDbModel> Create(TDbModel model)
    {
        await _entities.AddAsync(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<TDbModel> Update(TDbModel model)
    {
        var toUpdate = await _entities.FirstOrDefaultAsync(m => m.Id == model.Id);
        if (toUpdate != null)
        {
            toUpdate = model;
        }

        Context.Update(toUpdate);
        await Context.SaveChangesAsync();
        return toUpdate;
    }

    public async Task Delete(Guid id)
    {
        var toDelete = await _entities.FirstOrDefaultAsync(m => m.Id == id);
        _entities.Remove(toDelete);
        await Context.SaveChangesAsync();
    }
}