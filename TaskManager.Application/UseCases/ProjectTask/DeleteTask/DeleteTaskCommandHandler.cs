using MediatR;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.ProjectTask.DeleteTask;

public class DeleteTaskCommandHandler :  ICommandHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        await _taskRepository.Delete(request.Id);
        return await Result.SuccessAsync();
    }
}