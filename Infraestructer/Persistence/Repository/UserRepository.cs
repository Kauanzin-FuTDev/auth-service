using Domain.Entities;
using Domain.Repository;
using Infraestructer.Context;
using Microsoft.EntityFrameworkCore;


namespace Infraestructer.Persistence.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{   
    
    public UserRepository(AppDbContext context) : base(context){}
    
    public async Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
            
    }
    
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
    
}