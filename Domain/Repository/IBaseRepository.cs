using System.Linq.Expressions;

namespace Domain.Repository;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<List<T>> GetAllByFilter(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<bool> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(string id,T entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
}