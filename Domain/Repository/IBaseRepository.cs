using System.Linq.Expressions;

namespace Domain.Repository;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<T>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAllByFilter(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<bool> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> SuspendAsync(int id, CancellationToken cancellationToken);
}