using TaskManager.Domain.Entity;

namespace TaskManager.Infrastructure.Interfaces;

public interface ICommentRepository
{
    Task<Comment> Insert(Comment comment);
    Task<List<Comment>> GetByTask(string id);
}