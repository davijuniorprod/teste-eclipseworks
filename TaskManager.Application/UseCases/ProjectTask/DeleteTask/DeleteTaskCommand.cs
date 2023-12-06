using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.ProjectTask.DeleteTask;

public class DeleteTaskCommand : ICommand
{
    public string Id { get; set; }

    public DeleteTaskCommand(string id)
    {
        Id = id;
    }
}