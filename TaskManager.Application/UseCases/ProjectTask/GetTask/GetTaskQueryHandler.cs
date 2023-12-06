using System.Net;
using MongoDB.Bson;
using TaskManager.Application.Extensions;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.ProjectTask.GetTask;

public class GetTaskQueryHandler : IQueryHandler<GetTaskQuery, ProjectTaskViewModel>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<ProjectTaskViewModel>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.Id, out _))
            return await Result.FailureAsync<ProjectTaskViewModel>(null, "Invalid Task Id", HttpStatusCode.NotFound);
        
        var task = await _taskRepository.Get(request.Id);
        
        if(task == null)
            return await Result.FailureAsync<ProjectTaskViewModel>(null, "Task Not Found", HttpStatusCode.NotFound);
        
        var mapped = task.ToViewModel();
        
        return await Result.SuccessAsync(mapped);
    }
}