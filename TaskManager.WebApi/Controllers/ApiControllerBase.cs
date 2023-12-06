using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : Controller
{
    private readonly IServiceProvider _serviceProvider;
    protected ApiControllerBase(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    protected async Task<IActionResult> ExecuteCommandAsync<TRequest>(TRequest request)
        where TRequest : class, ICommand =>
        await ExecuteAsync<TRequest, Unit>(request);

    protected async Task<IActionResult> ExecuteCommandAsync<TRequest, TResult>(TRequest request)
        where TRequest : class, ICommand<TResult> =>
        await ExecuteAsync<TRequest, TResult>(request);

    protected async Task<IActionResult> ExecuteQueryAsync<TRequest, TResult>()
        where TRequest : class, IQuery<TResult>, new() =>
        await ExecuteAsync<TRequest, TResult>(new TRequest());

    protected async Task<IActionResult> ExecuteQueryAsync<TRequest, TResult>(TRequest request)
        where TRequest : class, IQuery<TResult> =>
        await ExecuteAsync<TRequest, TResult>(request);

    private async Task<IActionResult> ExecuteAsync<TRequest, TResult>(TRequest request)
        where TRequest : class, IRequest<Result<TResult>>
    {
        IActionResult actionResult;

        try
        {
            var validationResult = await Validate<TRequest, TResult>(request);

            if (validationResult.IsSuccess)
            {
                var mediator = _serviceProvider.GetRequiredService<IMediator>();
                var result = await mediator.Send(request);

                actionResult = StatusCode(result.StatusCode.GetHashCode(), result);
            }
            else
            {
                actionResult = BadRequest(validationResult);
            }
                
        }
        catch (Exception ex)
        {
            actionResult = StatusCode(StatusCodes.Status500InternalServerError,
                await Result.FailureAsync($"Error on try process your request.{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}"));
        }

        return actionResult;
    }

        
    public async Task<IActionResult> ExecuteLegacy<TRequest, TResult, TMapped>(TRequest request, Func<Result<TResult>, TMapped> map)
        where TRequest : class, IRequest<Result<TResult>>
    {
        IActionResult actionResult;

        try
        {
            var validationResult = await Validate<TRequest, TResult>(request);

            if (validationResult.IsSuccess)
            {
                var mediator = _serviceProvider.GetRequiredService<IMediator>();
                var result = await mediator.Send(request);

                actionResult = result.IsSuccess switch
                {
                    true => StatusCode(result.StatusCode.GetHashCode(), map(result)),
                    _ => StatusCode(result.StatusCode.GetHashCode(),
                        new { error = new { detail = result.FailureReason } })
                };
            }
            else
            {
                actionResult = BadRequest(new { errors = new[] { validationResult.FailureReason }});
            }
                
        }
        catch (Exception ex)
        {
            actionResult = StatusCode(StatusCodes.Status500InternalServerError,
                new {error = new { detail =$"Error on try process your request." }});
        }

        return actionResult;
    }
    private async Task<Result> Validate<TRequest, TResult>(TRequest request)
        where TRequest : class, IRequest<Result<TResult>>
    {
        var validator = _serviceProvider.GetService<IValidator<TRequest>>();

        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new {Field = e.PropertyName, Message = e.ErrorMessage})
                    .ToArray();
                
                return Result.Failure(new {ValidationErrors = errors}, "Invalid request, check the fields in the payload");
            }
        }

        return await Result.SuccessAsync();
    }
}
