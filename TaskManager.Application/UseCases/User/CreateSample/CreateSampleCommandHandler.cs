using System.Net;
using MediatR;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Domain.Enum;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.User.CreateSample;

public class CreateSampleCommandHandler : ICommandHandler<CreateSampleCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateSampleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<Unit>> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
    {
        var users = new List<Domain.Entity.User> {
            new ($"Davi {DateTime.Now:s}", Role.Developer),
            new ($"Alberto {DateTime.Now:s}", Role.Manager),
            new ($"Luiz {DateTime.Now:s}", Role.QA),
            new ($"Sandro {DateTime.Now:s}", Role.Developer),
            new ($"Mathias {DateTime.Now:s}", Role.DBA),
        };

        await _userRepository.InsertMany(users);

        return await Result.SuccessAsync(HttpStatusCode.Created);
    }
}