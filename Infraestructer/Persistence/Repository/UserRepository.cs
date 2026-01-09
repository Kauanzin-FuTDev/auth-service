using Domain.Entities;
using Domain.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infraestructer.Persistence.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context, "Users"){}
    
    
    public async Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _collection.Find(u => u.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _collection.Find(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
    }
    
}