using System.Linq.Expressions;
using Domain.Repository;

namespace Infraestructe.Persistence;

public class BaseRepository<T> : IBaseRepository<T> where T : class{
    public Task<bool> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllByFilter(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SuspendAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}