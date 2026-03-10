using Domain.Entities;
using Domain.Repository;
using Infraestructer.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructer.Persistence.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    protected RoleRepository(AppDbContext context) : base(context)
    {
    }

    public Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _context.Roles.FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }
}