using MongoDB.Driver;
using TaskManager.Domain.Entity;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Infrastructure.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly IMongoCollection<Comment> _collection;
    
    public CommentRepository(IMongoDatabase mongoDatabase) => _collection = mongoDatabase.GetCollection<Comment>(nameof(Comment));

    public async Task<Comment> Insert(Comment comment)
    {
        await _collection.InsertOneAsync(comment);
        return comment;
    }

    public async Task<List<Comment>> GetByTask(string id)
    {
        var comments = await _collection.Find(x => x.IdTask == id).ToListAsync();
        return comments;
    }
}