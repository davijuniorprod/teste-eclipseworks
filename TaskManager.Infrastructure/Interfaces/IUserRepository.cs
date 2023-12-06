using TaskManager.Domain.Entity;

namespace TaskManager.Infrastructure.Interfaces;

public interface IUserRepository
{
    public Task InsertMany(List<User> users);
    public Task<List<User>> GetUsers();
    public Task<User> Get(string id);
}