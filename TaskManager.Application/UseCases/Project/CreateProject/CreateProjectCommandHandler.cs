using System.Net;
using TaskManager.Application.Extensions;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.Project.CreateProject;

public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, ProjectViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository) => _projectRepository = projectRepository;

    public async Task<Result<ProjectViewModel>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.Create(new Domain.Entity.Project(
            request.Name, 
            request.Description
        ));
        var mapped = project.ToViewModel();
        return Result.Success(mapped, HttpStatusCode.Created);
    }
}