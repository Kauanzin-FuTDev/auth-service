using System.Linq.Expressions;
using Domain.Repository;
using Infraestructer.Context;
using Microsoft.EntityFrameworkCore;


namespace Infraestructer.Persistence.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class{
    
    protected readonly AppDbContext _context;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<List<T>> GetAllByFilter(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken);
        
    }
    
    public async Task<bool> UpdateAsync(string id,T entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<T>().FindAsync(id, cancellationToken);
        
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

   
    /*
     Ver a regra de negocio
    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
    {
       

       
    }
    */
}