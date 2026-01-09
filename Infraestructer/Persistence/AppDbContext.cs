using Domain.Entities;
using MongoDB.Driver;


namespace Infraestructer.Persistence;

public class AppDbContext 
{
    public IMongoDatabase Database { get; }

    public AppDbContext(MongoSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        Database = client.GetDatabase(settings.DatabaseName);
    }
}