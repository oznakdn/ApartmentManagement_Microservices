using MongoDB.Driver;
using Shared.Core.Abstracts;

namespace Notification.Infrastructure.Contexts;

public class MongoDbContext<TCollection> where TCollection : Entity
{
    private readonly IMongoCollection<TCollection> Collectoion;
    public MongoDbContext(MongoOption mongoOption)
    {
        var client = new MongoClient(mongoOption.ConnectionString);
        var database = client.GetDatabase(mongoOption.DatabaseName);
        Collectoion = database.GetCollection<TCollection>(typeof(TCollection).Name);
    }

    public async Task<TCollection> AddAsync(TCollection collection)
    {
        await Collectoion.InsertOneAsync(collection);
        return collection;
    }


    public async Task UpdateAsync(TCollection collection) => await Collectoion.ReplaceOneAsync(x => x.Id == collection.Id, collection);


    public async Task DeleteAsync(TCollection collection) => await Collectoion.DeleteOneAsync(x => x.Id == collection.Id);


    public async Task<TCollection> GetByIdAsync(string id) => await Collectoion.Find(x => x.Id == id).SingleOrDefaultAsync();


    public async Task<List<TCollection>> GetAllAsync() => await Collectoion.Find(_ => true).ToListAsync();


    public async Task<TCollection> GetByFilterAsync(FilterDefinition<TCollection> filter) => await Collectoion.Find(filter).FirstOrDefaultAsync();

}
