using System.Net;
using System.Text.Json.Serialization;
using MediatR;

namespace TaskManager.Core.Mediator;

public class Result
{
    protected Result(string failureReason = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        FailureReason = failureReason;
        StatusCode = statusCode;
    }

    public bool IsSuccess => FailureReason == null;
    public string FailureReason { get; protected set; }
        
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; protected set; }

    public static Result<T> Success<T>(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new Result<T>(result, statusCode : statusCode);

    public static Task<Result<T>> SuccessAsync<T>(T result, HttpStatusCode statusCode = HttpStatusCode.OK) 
        => Task.FromResult(new Result<T>(result, statusCode : statusCode));

    public static Result<Unit> Success(HttpStatusCode statusCode = HttpStatusCode.OK) 
        => Success(Unit.Value, statusCode);

    public static Task<Result<Unit>> SuccessAsync(HttpStatusCode statusCode = HttpStatusCode.OK) 
        => Task.FromResult(Success(statusCode));

    public static Result<T> Failure<T>(T result, string failureReason, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        => new Result<T>(result, failureReason, statusCode);

    public static Result<T> Failure<T>(string failureReason, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        => new Result<T>(default, failureReason, statusCode);

    public static Task<Result<T>> FailureAsync<T>(T result, string failureReason, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        => Task.FromResult(new Result<T>(result, failureReason, statusCode));

    public static Result<Unit> Failure(string failureReason, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        => Failure(Unit.Value, failureReason, statusCode);

    public static Task<Result<Unit>> FailureAsync(string failureReason, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        => Task.FromResult(Failure(Unit.Value, failureReason, statusCode));
}

[Serializable]
public class Result<T> : Result
{
    internal Result(T payload, string failureReason = null, HttpStatusCode statusCode = HttpStatusCode.OK) : base(failureReason, statusCode) 
        => Payload = payload;

    public T Payload { get; private set; }
}