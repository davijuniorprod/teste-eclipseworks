using System.Net;
using MongoDB.Bson;
using TaskManager.Application.Extensions;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Domain.Enum;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.ProjectTask.CreateTask;

public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, ProjectTaskViewModel>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(IProjectRepository projectRepository, ITaskRepository taskRepository)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
    }

    public async Task<Result<ProjectTaskViewModel>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.IdProject, out _))
            return await Result.FailureAsync<ProjectTaskViewModel>(null, "Invalid IdProject");

        var project = await _projectRepository.Get(request.IdProject);

        if (project == null)
            return await Result.FailureAsync<ProjectTaskViewModel>(null,"Project Not Found", HttpStatusCode.NotFound);

        var tasks = await _taskRepository.GetByProjectId(request.IdProject);
        
        if (tasks is { Count: >= 20 })
            return await Result.FailureAsync<ProjectTaskViewModel>(null,"Tasks Limit Has Exceeded", HttpStatusCode.Conflict);

        var task = new Domain.Entity.ProjectTask(request.IdProject, request.IdUser, request.Title, request.Description, request.DueDate, (Priority)request.Priority);
        var mapped = task.ToViewModel();

        await _taskRepository.Insert(task);

        return await Result.SuccessAsync(mapped, HttpStatusCode.Created);
    }
}