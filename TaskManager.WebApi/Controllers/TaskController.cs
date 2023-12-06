using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.ProjectTask.CreateTask;
using TaskManager.Application.UseCases.ProjectTask.DeleteTask;
using TaskManager.Application.UseCases.ProjectTask.GetTask;
using TaskManager.Application.UseCases.ProjectTask.ListTasks;
using TaskManager.Application.UseCases.ProjectTask.UpdateTask;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.WebApi.Controllers;

public class TaskController : ApiControllerBase
{
    public TaskController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Result<ProjectTaskViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Create([FromBody] CreateTaskCommand request) 
        => ExecuteCommandAsync<CreateTaskCommand, ProjectTaskViewModel>(request);
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Result<ProjectTaskViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateTaskRequest request) 
        => ExecuteCommandAsync<UpdateTaskCommand, ProjectTaskViewModel>(new UpdateTaskCommand(id, request));
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Delete([FromRoute] string id) 
        => ExecuteCommandAsync(new DeleteTaskCommand(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ProjectTaskViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Update([FromRoute] string id) 
        => ExecuteQueryAsync<GetTaskQuery, ProjectTaskViewModel>(new GetTaskQuery(id));

    [HttpGet]
    [ProducesResponseType(typeof(IPagedQuery<ListTasksPagedQuery>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> ListPaged(string idProject, int index = 1, int size = 5) 
        => ExecuteQueryAsync<ListTasksPagedQuery, PagedResult<ProjectTaskViewModel>>(new ListTasksPagedQuery(idProject, index, size));
}