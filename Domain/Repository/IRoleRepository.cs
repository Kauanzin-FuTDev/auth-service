using Domain.Entities;

namespace Domain.Repository;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
}