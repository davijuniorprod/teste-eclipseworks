using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.User.CreateSample;
using TaskManager.Core.Handlers;

namespace TaskManager.WebApi.Controllers;

public class UserController : ApiControllerBase
{
    public UserController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    
    [HttpGet("create-sample")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreateSample() 
        => ExecuteCommandAsync(new CreateSampleCommand());
}