using System.Net;
using MediatR;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Domain.Enum;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.Project.DeleteProject;

public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository, ITaskRepository taskRepository)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.Get(request.Id);

        if (project == null)
            return await Result.FailureAsync("Project Not Found", HttpStatusCode.NotFound);

        var tasks = await _taskRepository.GetByProjectId(request.Id);
        
        if (tasks != null && tasks.Any(x => x.Status != Status.Done))
            return await Result.FailureAsync("Project Has Tasks To Be Finished", HttpStatusCode.Conflict);

        await _projectRepository.Delete(request.Id);
        
        return await Result.SuccessAsync();
    }
}