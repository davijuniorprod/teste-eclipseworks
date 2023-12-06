using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.ProjectTask.GetTask;

public class GetTaskQuery :  IQuery<ProjectTaskViewModel>
{
    public string Id { get; set; }

    public GetTaskQuery(string id)
    {
        Id = id;
    }
}