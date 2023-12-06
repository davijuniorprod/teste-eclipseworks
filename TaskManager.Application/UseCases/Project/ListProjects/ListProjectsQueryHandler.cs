using TaskManager.Application.Extensions;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.Project.ListProjects;

public class ListProjectsQueryHandler : IQueryHandler<ListProjectsQuery, List<ProjectViewModel>>
{
    private readonly IProjectRepository _projectRepository;

    public ListProjectsQueryHandler(IProjectRepository projectRepository) => _projectRepository = projectRepository;

    public async Task<Result<List<ProjectViewModel>>> Handle(ListProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.List();
        var mapped = projects.Select(ViewModelExtensions.ToViewModel).ToList();
        return Result.Success(mapped);
    }
}