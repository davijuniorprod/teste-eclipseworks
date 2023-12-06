using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Domain.Entity;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Infrastructure.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<ProjectTask> _collection;
    
    public TaskRepository(IMongoDatabase mongoDatabase) => _collection = mongoDatabase.GetCollection<ProjectTask>(nameof(ProjectTask));
    
    public async Task<Tuple<List<ProjectTask>, int>> GetPaged(string projectId, int index, int size)
    {
        var taskCount = await _collection.Find(x => x.IdProject == projectId).CountDocumentsAsync();
        var tasks = await _collection
            .Find(x => x.IdProject == projectId)
            .Skip((index - 1) * size)
            .Limit(size)
            .ToListAsync();
 
        return new Tuple<List<ProjectTask>, int>(tasks, (int)taskCount);
    }

    public async Task<ProjectTask> Get(string id)
    {
        var task = await _collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        return task;
    }

    public async Task<List<ProjectTask>> GetByProjectId(string id)
    {
        var tasks = await _collection.Find(x => x.IdProject == id).ToListAsync();
        return tasks;
    }

    public async Task<ProjectTask> Update(ProjectTask task)
    {
        var builder = Builders<ProjectTask>.Filter;
        var filter = builder.Eq(x => x.Id, task.Id);
        await _collection.ReplaceOneAsync(filter, task);
        return task;
    }

    public async Task Delete(string id)
    {
        var builder = Builders<ProjectTask>.Filter;
        var filter = builder.Eq(x => x.Id, ObjectId.Parse(id));
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<ProjectTask> Insert(ProjectTask task)
    {
        await _collection.InsertOneAsync(task);
        return task;
    }
}