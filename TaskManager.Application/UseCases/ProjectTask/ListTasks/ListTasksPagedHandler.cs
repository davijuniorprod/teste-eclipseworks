using System.Net;
using MongoDB.Bson;
using TaskManager.Application.Extensions;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.ProjectTask.ListTasks;

public class ListTasksPagedHandler : IPagedQueryHandler<ListTasksPagedQuery, ProjectTaskViewModel>
{
    private readonly ITaskRepository _taskRepository;

    public ListTasksPagedHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<PagedResult<ProjectTaskViewModel>>> Handle(ListTasksPagedQuery request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.IdProject, out _))
            return await Result.FailureAsync<PagedResult<ProjectTaskViewModel>>(null, "Invalid IdProject", HttpStatusCode.NotFound);

        var response = await _taskRepository.GetPaged(request.IdProject, request.Index, request.Size);

        if (response == null)
            return await Result.FailureAsync<PagedResult<ProjectTaskViewModel>>(null,"Project Not Found", HttpStatusCode.NotFound);

        var tasks = response.Item1;
        var mapped = tasks.Select(x => x.ToViewModel());
        var totalCount = response.Item2;
        var result = new PagedResult<ProjectTaskViewModel>(mapped, request.PageSettings.Current(totalCount));
        
        return Result.Success(result);
    }
}