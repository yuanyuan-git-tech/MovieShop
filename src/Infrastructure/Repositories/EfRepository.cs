using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class EfRepository<T>: IRepository<T> where T : class
{
    protected readonly MovieShopDbContext _dbContext;

    public EfRepository(MovieShopDbContext movieShopDbContext)
    {
        _dbContext = movieShopDbContext;
    }
    public virtual Task<T> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<T> Add(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }
}