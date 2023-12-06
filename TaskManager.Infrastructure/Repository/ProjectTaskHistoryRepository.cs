using MongoDB.Driver;
using TaskManager.Domain.Entity;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Infrastructure.Repository;

public class ProjectTaskHistoryRepository : IProjectTaskHistoryRepository
{
    private readonly IMongoCollection<ProjectTaskHistory> _collection;
    
    public ProjectTaskHistoryRepository(IMongoDatabase mongoDatabase) => _collection = mongoDatabase.GetCollection<ProjectTaskHistory>(nameof(ProjectTaskHistory));
    
    public async Task Insert(ProjectTaskHistory projectTaskHistory)
    {
        await _collection.InsertOneAsync(projectTaskHistory);
    }
}