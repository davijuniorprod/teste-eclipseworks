using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Domain.Enum;

namespace TaskManager.Application.UseCases.ProjectTask.UpdateTask;

public class UpdateTaskCommand : ICommand<ProjectTaskViewModel>
{
    public string Id { get; set; }
    public string IdUser { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public Status Status { get; set; }

    public UpdateTaskCommand(string id, UpdateTaskRequest request)
    {
        Id = id;
        IdUser = request.IdUser;
        Title = request.Title;
        Description = request.Description;
        DueDate = request.DueDate;
        Status = (Status)request.Status;
    }
}