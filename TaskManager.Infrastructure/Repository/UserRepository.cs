using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Domain.Entity;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;
    
    public UserRepository(IMongoDatabase mongoDatabase) => _collection = mongoDatabase.GetCollection<User>(nameof(User));

    public async Task InsertMany(List<User> users)
    {
        await _collection.InsertManyAsync(users);
    }

    public async Task<List<User>> GetUsers()
    {
        var users = await _collection.Find(_ => true).ToListAsync();
        return users;
    }

    public async Task<User> Get(string id)
    {
        var user = await _collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        return user;
    }
}