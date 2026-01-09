using Domain.Entities;

namespace Domain.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}