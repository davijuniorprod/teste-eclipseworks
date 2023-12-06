using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Comment.CreateComment;
using TaskManager.Application.UseCases.Comment.GetComments;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Domain.Entity;

namespace TaskManager.WebApi.Controllers;

public class CommentController : ApiControllerBase
{
    public CommentController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Create([FromBody] CreateCommentCommand request) 
        => ExecuteCommandAsync(request);
    
    [HttpGet]
    [ProducesResponseType(typeof(Result<List<CommentViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetByTask(string idTask) 
        => ExecuteQueryAsync<GetCommentsQuery, List<CommentViewModel>>(new GetCommentsQuery(idTask));
}