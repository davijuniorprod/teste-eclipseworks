using System.Net;
using MediatR;
using MongoDB.Bson;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.Comment.CreateComment;

public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ITaskRepository taskRepository, IUserRepository userRepository, ICommentRepository commentRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
    }

    public async Task<Result<Unit>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.IdTask, out _))
            return await Result.FailureAsync("Invalid IdTask");

        if (!ObjectId.TryParse(request.IdUser, out _))
            return await Result.FailureAsync("Invalid IdUser");
        
        if (string.IsNullOrEmpty(request.Comment))
            return await Result.FailureAsync("Invalid Comment");

        var user = await _userRepository.Get(request.IdUser);
        
        if (user == null)
            return await Result.FailureAsync("User Not Found");
        
        var task = await _taskRepository.Get(request.IdTask);
        
        if (task == null)
            return await Result.FailureAsync("Task Not Found");

        var comment = new Domain.Entity.Comment(request.IdTask, request.IdUser, user.Name, user.Role.ToString(), request.Comment);
        
        await _commentRepository.Insert(comment);

        return await Result.SuccessAsync(HttpStatusCode.Created);
    }
}