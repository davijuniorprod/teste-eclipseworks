using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Project.CreateProject;
using TaskManager.Application.UseCases.Project.DeleteProject;
using TaskManager.Application.UseCases.Project.GetProject;
using TaskManager.Application.UseCases.Project.ListProjects;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;

namespace TaskManager.WebApi.Controllers;


public class ProjectController : ApiControllerBase
{
    public ProjectController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<List<ProjectViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> List() 
        => ExecuteQueryAsync<ListProjectsQuery, List<ProjectViewModel>>(new ListProjectsQuery());
    
    [HttpPost]
    [ProducesResponseType(typeof(Result<ProjectViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Create([FromBody] CreateProjectCommand request) 
        => ExecuteCommandAsync<CreateProjectCommand, ProjectViewModel>(request);

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ProjectViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Get([FromRoute] string id) 
        => ExecuteQueryAsync<GetProjectQuery, ProjectViewModel>(new GetProjectQuery(id));
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Delete([FromRoute] string id) 
        => ExecuteCommandAsync(new DeleteProjectCommand(id));
}