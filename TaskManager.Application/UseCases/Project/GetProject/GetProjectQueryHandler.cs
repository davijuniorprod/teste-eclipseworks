using System.Net;
using MongoDB.Bson;
using TaskManager.Application.Extensions;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.Project.GetProject;

public class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, ProjectViewModel>
{
    private readonly IProjectRepository _projectRepository;
    
    public GetProjectQueryHandler(IProjectRepository projectRepository) => _projectRepository = projectRepository;

    public async Task<Result<ProjectViewModel>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.Id, out _))
            return await Result.FailureAsync<ProjectViewModel>(null, "Invalid IdProject", HttpStatusCode.NotFound);
        
        var project = await _projectRepository.Get(request.Id);
        
        if (project == null)
            return await Result.FailureAsync<ProjectViewModel>(null, "Project Not Found", HttpStatusCode.NotFound);

        
        var mapped = project.ToViewModel();
        return Result.Success(mapped);
    }
}