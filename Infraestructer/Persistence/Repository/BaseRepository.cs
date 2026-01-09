using System.Linq.Expressions;
using Domain.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infraestructer.Persistence.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class{
    
    protected readonly IMongoCollection<T> _collection;

    protected BaseRepository(AppDbContext context, string collectionName)
    {
        _collection = context.Database.GetCollection<T>(collectionName);
    }
    
    public async Task<bool> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return true;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _collection
            .Find(FilterDefinition<T>.Empty)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<T>> GetAllByFilter(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var objectId = new ObjectId(id);

        var filter = Builders<T>.Filter.Eq("_id", objectId);

        return await _collection
            .Find(filter)
            .FirstOrDefaultAsync(cancellationToken);
        
    }
    
    public async Task<bool> UpdateAsync(string id,T entity, CancellationToken cancellationToken)
    {
        var objectId = new ObjectId(id);

        var result = await _collection.ReplaceOneAsync(
            Builders<T>.Filter.Eq("_id", objectId),
            entity,
            cancellationToken: cancellationToken
        );

        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var objectId = new ObjectId(id);

        var result = await _collection.DeleteOneAsync(
            Builders<T>.Filter.Eq("_id", objectId),
            cancellationToken
        );

        return result.DeletedCount > 0;
    }
}