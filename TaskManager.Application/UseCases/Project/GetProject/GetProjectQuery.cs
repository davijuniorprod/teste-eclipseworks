using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.Project.GetProject;

public class GetProjectQuery :  IQuery<ProjectViewModel>
{
    public string Id { get; set; }

    public GetProjectQuery(string id)
    {
        Id = id;
    }
}