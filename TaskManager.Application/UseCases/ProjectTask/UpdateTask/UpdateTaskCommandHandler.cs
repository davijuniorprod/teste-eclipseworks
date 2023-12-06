using MongoDB.Bson;
using TaskManager.Application.Extensions;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Domain.Entity;
using TaskManager.Domain.Enum;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.ProjectTask.UpdateTask;

public class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand, ProjectTaskViewModel>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IProjectTaskHistoryRepository _projectTaskHistoryRepository;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository, IUserRepository userRepository, ICommentRepository commentRepository, IProjectTaskHistoryRepository projectTaskHistoryRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _projectTaskHistoryRepository = projectTaskHistoryRepository;
    }

    public async Task<Result<ProjectTaskViewModel>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.Id, out _))
            return await Result.FailureAsync<ProjectTaskViewModel>(null, "Invalid IdTask");

        if (!ObjectId.TryParse(request.IdUser, out _))
            return await Result.FailureAsync<ProjectTaskViewModel>(null, "Invalid IdUser");

        var currentTask = await _taskRepository.Get(request.Id);
        
        if (currentTask == null)
            return await Result.FailureAsync<ProjectTaskViewModel>(null, "Task Not Found");

        var user = await _userRepository.Get(request.IdUser);
        
        if (user == null)
            return await Result.FailureAsync<ProjectTaskViewModel>(null, "User Not Found");
        
        var updatedTask = currentTask.Clone();
        
        updatedTask.IdUser = request.IdUser;
        updatedTask.Title = request.Title;
        updatedTask.Description = request.Description;
        updatedTask.DueDate = request.DueDate;
        updatedTask.Status = request.Status;
        updatedTask.FinishDate = request.Status == Status.Done ? DateTime.Now : null;

        var updated = await _taskRepository.Update(updatedTask);
        var mapped = updated.ToViewModel();
        var comments = await _commentRepository.GetByTask(request.Id);
        var taskHistory = new ProjectTaskHistory(request.Id, user.Id.ToString(), user.Name, user.Role.ToString(), updatedTask, currentTask, comments);
        
        await _projectTaskHistoryRepository.Insert(taskHistory);

        return await Result.SuccessAsync(mapped);
    }
}