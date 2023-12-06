using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Domain.Entity;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Infrastructure.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly IMongoCollection<Project> _collection;
    
    public ProjectRepository(IMongoDatabase mongoDatabase) => _collection = mongoDatabase.GetCollection<Project>(nameof(Project));

    public async Task<Project> Get(string id) => await _collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
    public async Task Delete(string id) => await _collection.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
    public async Task<List<Project>> List() => await _collection.Find(_ => true).ToListAsync();
    public async Task<Project> Create(Project project)
    {
        await _collection.InsertOneAsync(project);
        return project;
    }
    public async Task<Project> Update(Project project)
    {
        await _collection.ReplaceOneAsync(x => x.Id == project.Id, project);
        return project;
    }

    
}